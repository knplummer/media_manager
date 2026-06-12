using MediaManager.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MediaManager.Infrastructure.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    // For now, we hardcode the current user ID to 1 since we don't have an ICurrentUserService yet.
    // In a real application, inject ICurrentUserService to get the actual user ID.
    private const int SystemUserId = 1;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var now = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = SystemUserId;
                entry.Entity.CreatedDate = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedBy = SystemUserId;
                entry.Entity.UpdatedDate = now;
            }
        }

        foreach (var entry in context.ChangeTracker.Entries<IAuditableCreationEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = SystemUserId;
                entry.Entity.CreatedDate = now;
            }
        }
    }
}
