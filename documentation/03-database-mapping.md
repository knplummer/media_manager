# 03. Database Mapping and Migrations

## Overview
The Media Manager application uses PostgreSQL and Entity Framework Core (EF Core). The database schema baseline is defined in `scripts/sql/media_manager_schema.sql`. This document outlines the definitive rules for mapping C# entities to the database schema, maintaining database state, and managing auditing fields.

## Implementation Guidelines

### 1. Fluent API Configuration
Entity classes (e.g., `LibraryBucket`, `Media`) must represent pure domain models. 
* **DO NOT** use Data Annotations (e.g., `[Table]`, `[Column]`, `[ForeignKey]`) on entity classes.
* All database mapping rules (table names, column types, limits, foreign keys) must be strictly defined using EF Core's **Fluent API** via `IEntityTypeConfiguration<T>` classes.

### 2. Migration Strategy
Database schema updates are managed via standard **EF Core Migrations**.
* The baseline database is represented by the initial SQL file.
* C# models are kept perfectly synchronized with this baseline.
* The first migration should be considered a "baseline sync" and marked as applied on existing environments. Future modifications to entity models will generate standard EF Core Code-First migrations.

### 3. Automatic Auditing
Numerous tables contain standard tracking fields: `CreatedBy`, `CreatedDate`, `UpdatedBy`, and `UpdatedDate`.
* **DO NOT** manually assign these properties inside feature handlers or use cases.
* **DO** utilize an EF Core `SaveChangesInterceptor` (or override `SaveChangesAsync` in the `DbContext`) to automatically inject the current UTC time and the current authenticated user's ID into these properties whenever an entity is added or modified.

## Example: Entity Configuration
```csharp
namespace MediaManager.Api.Infrastructure.Persistence.Configurations;

public class LibraryBucketConfiguration : IEntityTypeConfiguration<LibraryBucket>
{
    public void Configure(EntityTypeBuilder<LibraryBucket> builder)
    {
        builder.ToTable("LibraryBuckets");

        builder.HasKey(e => e.LibraryBucketId);

        builder.Property(e => e.GroupType)
            .HasMaxLength(25)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasMaxLength(250)
            .IsRequired();

        // Strict Foreign Key Configurations
        builder.HasOne(d => d.Creator)
            .WithMany()
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryBucket_CreatedBy_Users_UserId");
    }
}
```
