using MediaManager.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class LibraryBucketConfiguration : IEntityTypeConfiguration<LibraryBucket>
{
    public void Configure(EntityTypeBuilder<LibraryBucket> builder)
    {
        builder.ToTable("LibraryBuckets");
        builder.HasKey(e => e.LibraryBucketId);

        builder.Property(e => e.GroupType).HasMaxLength(25).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(250).IsRequired();
        builder.Property(e => e.Metadata).HasMaxLength(750);

        builder.HasOne(d => d.Creator)
            .WithMany()
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryBucket_CreatedBy_Users_UserId");

        builder.HasOne(d => d.Updater)
            .WithMany()
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryBucket_UpdatedBy_Users_UserId");
    }
}
