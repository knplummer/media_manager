using MediaManager.Shared.Domain.Models;
using MediaManager.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Infrastructure.Persistence;

public class MediaManagerDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public MediaManagerDbContext(
        DbContextOptions<MediaManagerDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Media> Media => Set<Media>();
    public DbSet<MediaHistory> MediaHistory => Set<MediaHistory>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<LibraryBucket> LibraryBuckets => Set<LibraryBucket>();
    public DbSet<LibraryItem> LibraryItems => Set<LibraryItem>();
    public DbSet<LibrarySubItem> LibrarySubItems => Set<LibrarySubItem>();
    public DbSet<AccessRequest> AccessRequests => Set<AccessRequest>();
    public DbSet<LibraryRequest> LibraryRequests => Set<LibraryRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediaManagerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}
