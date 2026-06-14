using MediaManager.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class AccessRequestConfiguration : IEntityTypeConfiguration<AccessRequest>
{
    public void Configure(EntityTypeBuilder<AccessRequest> builder)
    {
        builder.ToTable("AccessRequests");
        builder.HasKey(e => e.UserRequestId);

        builder.Property(e => e.Note).HasMaxLength(1000);
        builder.Property(e => e.IsDenied).IsRequired();

        builder.HasOne(d => d.Creator)
            .WithMany()
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("AccessRequests_CreatedBy_Users_UserId");

        builder.HasOne(d => d.Updater)
            .WithMany()
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("AccessRequests_UpdatedBy_Users_UserId");
    }
}
