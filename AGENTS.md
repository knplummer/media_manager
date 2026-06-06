# AI Agent Instructions for Media Manager

This file contains the foundational context, architecture decisions, and coding guidelines for AI agents working on the **Media Manager** repository. Always adhere to these principles when generating code or proposing changes.

## 1. Project Context
Media Manager is a unified cataloging system for physical and digital media. It securely preserves massive digital files in the cloud (AWS S3, Backblaze B2, Google Cloud Storage) and uses a **Smart Sync Daemon** running on a local home server (Plex/Jellyfin) to rotate a limited subset of these files onto local disks.

## 2. Technology Stack
* **Backend:** C# / .NET Core (Targeting .NET 8 LTS or newer).
* **Database / ORM:** PostgreSQL using **Entity Framework Core (EF Core)**.
* **API Framework:** ASP.NET Core **Minimal APIs** (Do not use MVC Controllers).
* **Messaging:** **MassTransit** (In-memory transport to start).

## 3. Architecture Pattern: Clean Vertical Slices
* **Structure:** The backend is organized by feature slices (e.g., `Features/LibraryBuckets/CreateBucket`), not technical layers.
* **Separation of Concerns within Slices:**
  * **Endpoints:** Map HTTP requests to commands/queries.
  * **Handlers:** Contain business logic. Must use Dependency Injection (DI). Do NOT instantiate concrete external services directly.
  * **Validators:** Use FluentValidation.
* **Independence:** Slices must not reference each other's handlers directly.

## 4. Database Mapping & Auditing
* **Configurations:** Use EF Core **Fluent API** configuration classes (`IEntityTypeConfiguration<T>`). Do not use Data Annotations on domain entities to keep them clean.
* **Migrations:** We use EF Core Code-First migrations, bootstrapping from the initial `media_manager_schema.sql` baseline.
* **Auditing:** Do not set `CreatedBy`, `CreatedDate`, `UpdatedBy`, etc., manually in handlers. These must be populated automatically by overriding `SaveChangesAsync` in the `DbContext` or using an EF Core Interceptor.

## 5. Cloud Storage & Massive Uploads
* **Multi-Provider Support:** Providers are configured in `appsettings.json`. The application routes uploads dynamically based on file size and quotas.
* **Streaming Proxy:** The Web API must **stream** file uploads directly to the chosen cloud provider using `AWSSDK.S3`. **Do not buffer massive media files (e.g., 50GB rips) into server memory or local disk.**
* **Database Tracking:** The chosen provider's ID is saved in the `Media` table under the `ExternalStorageKey` column.

## 6. Sync Daemon Boundaries
* **Deployment:** The Sync Daemon is a separate .NET `Worker Service` project. It runs physically on the home server.
* **Data Access:** The Daemon **does not connect to the database directly**. It acts as an HTTP client fetching data from the Web API.
* **Communication:** The Web API sends real-time push events to the Daemon via **SignalR (WebSockets)** or AMQPS to wake it up instantly.
* **Logic Boundary:** 
  * The Web API is the **"Centralized Brain"**—it evaluates metadata and decides exactly what gets evicted.
  * The Sync Daemon is a **"Dumb Worker"**—it reports local disk space and executes "Download File" or "Delete File" commands on the local hardware.

## 7. Event-Driven Communication
* **Inter-Slice Communication:** Use **MassTransit** to publish integration events (e.g., `MediaIngestedEvent`).
* **Decoupling:** Use this mechanism to trigger secondary actions (like logging history or triggering sync updates) without tightly coupling feature slices together.

## 8. Authentication & Authorization
* **Authentication:** Handled externally by an OIDC Provider (e.g., Auth0, Entra ID). The API expects JWT Bearer tokens.
* **Authorization:** Local. The API maps the OIDC `sub` claim to a user in the local `Users` table and evaluates their database-driven `Roles` and `Permissions` via ASP.NET Core Authorization Policies.

## 9. Coding Guidelines
* Always provide mockable interfaces for external I/O (Clock, FileSystem, Cloud APIs).
* Maintain strict adherence to C# nullable reference types (`<Nullable>enable</Nullable>`).
* Keep endpoints lightweight; push complex orchestrations into MediatR/MassTransit handlers.
* When working with the file system for the Sync Daemon, account for transient networking failures (cloud downloads) with robust retry policies (e.g., Polly).
