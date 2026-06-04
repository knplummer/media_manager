# Media Manager

Media Manager is a unified application designed to catalogue physical and digital media libraries, safely preserve digital backups in the cloud, and dynamically synchronize a rotating queue of media files to a local home media server (e.g., Plex, Jellyfin, or audio/book servers) with limited physical storage.

---

## 🚀 Core Features

### 1. Unified Media Cataloging
* **Any Format Support:** Catalog books, movies, TV series, music, or retro games.
* **Physical & Digital Inventory:** Distinguish between physical Blu-rays, paperbacks, Kindle books, and purchases from digital storefronts (Steam, Apple, Google, etc.).
* **Hierarchy:** Organizes collections using a nested system of Buckets, Items, and Sub-items (e.g., *TV Show Series* -> *Seasons* -> *Episodes*).

### 2. Multi-Cloud Preservation
* **Digital Backups:** Upload full-quality digital copies directly via the web interface.
* **Cloud Storage Providers:** Securely back up media to cloud providers (e.g., AWS S3, Backblaze B2, Google Cloud Storage).
* **On-Demand Retrieval:** Retrieve or download original preservation copies directly from cloud storage whenever needed.

### 3. Automated Media Server Rotation (Smart Sync)
* **Storage Optimization:** Solves the problem of local media servers having less disk space than the cloud library.
* **Intelligent Eviction:** Automatically monitors local disk usage and deletes files from the local media server once they are no longer in active rotation, while keeping them safe in the cloud.
* **Always-Available Pinning:** Allows users to mark specific media as "always available" to prevent automatic deletion from the local hard drive.
* **Continuous Syncing:** Rotates the selection on a schedule or trigger, ensuring a fresh pool of media is available locally.

---

## 📐 Conceptual Architecture

```mermaid
graph TD
    User([User / Client]) -->|Manage Catalog & Upload| UI[Web Interface]
    UI -->|Metadata & File Tracking| DB[(SQL Database)]
    UI -->|Archival Upload| Cloud[Cloud Storage - S3/B2]

    subgraph Home Server (Limited Storage)
        MS[Media Server - Plex/Jellyfin]
        Sync[Rotation & Sync Daemon]
        HD[(Local Hard Drive)]
    end

    Sync -->|Checks Queue / Local Space| DB
    Sync -->|Pull Down On-Demand| Cloud
    Sync -->|Manage Files| HD
    MS -.->|Read Media| HD
```

---

## 🗃️ Database Mapping

The database schema under [media_manager_schema.sql](file:///home/knplummer/source/repos/media_manager/scripts/sql/media_manager_schema.sql) supports these operations directly:

* **Cataloging Engine:**
  * `LibraryBucket` tracks the collection groupings (e.g., TV Shows, Movie series).
  * `LibraryItems` and `LibrarySubItems` store format information (`OwnedFormat`) and metadata.
* **Cloud and Server Tracking:**
  * `Media` acts as the file registry, tracking properties like `FileSize`, `ExternalStorageKey` (Cloud Reference), and `StorageGuid`.
  * `IsAlwaysAvailable` guarantees files are excluded from local rotation.
  * `IsOnMediaServer` flags whether a copy is actively present on the home media server's local disk.
* **Auditability:**
  * `MediaHistory` logs all server actions (e.g., `UPLOAD`, `DOWNLOAD`, `ROTATE_OFF`, `EVICT`) for transparent management of the rotation daemon.

---

## 🛠️ Getting Started & Setup

This repository contains a Docker Compose environment for local PostgreSQL development.

### 1. Start the Local PostgreSQL Database
Run Docker Compose to spin up the PostgreSQL container (configured to run on port `5433` with the database `mediamanager_dev`):
```bash
docker compose -f docker/dev/database/docker-compose.yaml up -d
```

### 2. Initialize the Database Schema
Apply the SQL schema under [media_manager_schema.sql](file:///home/knplummer/source/repos/media_manager/scripts/sql/media_manager_schema.sql) to set up all tables and constraints:
```bash
# Option A: Using a local psql client (port 5433, no password required)
psql -h localhost -p 5433 -U postgres -d mediamanager_dev -f scripts/sql/media_manager_schema.sql

# Option B: Executing it directly inside the running container
docker exec -i dev_postgres_container psql -U postgres -d mediamanager_dev < scripts/sql/media_manager_schema.sql
```
