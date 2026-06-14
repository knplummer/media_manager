using MediaManager.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class LibraryRequestConfiguration : IEntityTypeConfiguration<LibraryRequest>
{
    public void Configure(EntityTypeBuilder<LibraryRequest> builder)
    {
        builder.ToTable("LibraryRequests");
        builder.HasKey(e => e.LibraryRequestId);

        builder.Property(e => e.RequestTitle).HasMaxLength(250).IsRequired();
        builder.Property(e => e.RelevantLink).HasMaxLength(750);
        builder.Property(e => e.Note).HasMaxLength(1000);

        builder.HasOne(d => d.Creator)
            .WithMany()
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryRequests_CreatedBy_Users_UserId");

        builder.HasOne(d => d.Updater)
            .WithMany()
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryRequests_UpdatedBy_Users_UserId");
    }
}
