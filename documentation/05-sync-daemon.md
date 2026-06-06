# 05. Rotation & Sync Daemon

## Overview
The Smart Sync feature solves the storage limits of the local home media server (Plex/Jellyfin). The **Rotation & Sync Daemon** runs physically on the home server, downloading media files from cloud storage to the local disk and evicting old files based on commands from the Web API.

## Implementation Guidelines

### 1. Deployment Architecture
* The Daemon must be built as a separate, lightweight .NET `Worker Service` (`BackgroundService`).
* It must not host an HTTP server or Minimal API endpoints.

### 2. Security & Communication Boundaries
* **No Direct DB Access:** The Daemon must **never** connect directly to the PostgreSQL database. It must act as a standard HTTP Client.
* **Real-Time Push Events:** The Daemon maintains a secure, persistent connection to the Web API via **SignalR (WebSockets)** or a secure message broker (AMQPS).
* **HTTP API Polling Backup:** The Daemon fetches its required queue via secure HTTP requests (`GET /api/sync/queue`) when triggered by a push event.

### 3. Separation of Concerns (Brain vs. Worker)
* **The Web API (Centralized Brain):** The API owns all metadata. It evaluates rules (e.g., "Is it pinned?", "Was it played recently?") and calculates exactly what files should be evicted when space is needed.
* **The Sync Daemon (Dumb Worker):** The Daemon only performs local file system reads and writes. It reports disk usage (`DriveInfo`) to the API. It executes direct `Download(FileId)` or `Delete(FileId)` commands sent by the API.

## Local File Management Rules
* The Daemon requires its own `appsettings.json` to configure local storage paths (where Plex/Jellyfin expects the files to reside).
* When instructed to download a file, the Daemon reads the `ExternalStorageKey` provided by the API, finds the matching cloud provider configuration, and pulls the file directly from S3/B2 to the local drive.
* Transient errors (network drops during a 50GB download) must be handled gracefully using retry policies (e.g., Polly).
