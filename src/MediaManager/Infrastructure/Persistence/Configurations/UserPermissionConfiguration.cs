using MediaManager.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("UserPermissions");
        builder.HasKey(e => e.UserPermissionId);

        builder.HasOne(d => d.User)
            .WithMany(p => p.UserPermissions)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("UserPermissions_UserId_Users_UserId");

        builder.HasOne(d => d.Permission)
            .WithMany(p => p.UserPermissions)
            .HasForeignKey(d => d.PermissionId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("UserPermissions_PermissionId_Permissions_PermissionId");

        builder.HasOne(d => d.Creator)
            .WithMany()
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("UserPermissions_CreatedBy_Users_UserId");
    }
}
