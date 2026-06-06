# 01. Technology Stack

## Overview
The **Media Manager** backend is designed to be a high-performance, container-ready application capable of managing metadata, streaming massive file uploads, and supporting background synchronization.

## Backend Stack
The system is built exclusively on the following technologies:

1. **Language & Framework:** C# on .NET Core (targeting .NET 8 LTS or newer).
2. **Database & ORM:** PostgreSQL accessed via **Entity Framework Core (EF Core)**.
3. **API Framework:** ASP.NET Core **Minimal APIs**.

## Implementation Rules

### Use Minimal APIs Only
* Do **not** use traditional ASP.NET Core MVC Controllers.
* All HTTP routes must be defined using Minimal API endpoint extensions (e.g., `app.MapGet()`, `app.MapPost()`).
* Endpoints should be grouped by feature and registered dynamically to keep `Program.cs` clean.

### Use Entity Framework Core
* Raw SQL queries should be avoided unless absolutely necessary for advanced performance tuning.
* Use EF Core's `DbContext` and `DbSet<T>` for all data persistence and querying operations to leverage LINQ and unit-of-work tracking.

### C# Language Features
* Ensure nullable reference types (`<Nullable>enable</Nullable>`) are enabled across all projects to catch null-reference errors at compile time.
* Use modern C# features such as `record` types for data transfer objects (DTOs) and commands/queries.
