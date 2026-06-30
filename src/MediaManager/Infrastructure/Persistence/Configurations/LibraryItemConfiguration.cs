using MediaManager.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class LibraryItemConfiguration : IEntityTypeConfiguration<LibraryItem>
{
    public void Configure(EntityTypeBuilder<LibraryItem> builder)
    {
        builder.ToTable("LibraryItems");
        builder.HasKey(e => e.LibraryItemId);

        builder.Property(e => e.ItemType).HasMaxLength(25).IsRequired();
        builder.Property(e => e.OwnedFormat).HasMaxLength(25);
        builder.Property(e => e.Name).HasMaxLength(25);
        builder.Property(e => e.Metadata).HasMaxLength(750);

        builder.HasOne(d => d.LibraryBucket)
            .WithMany(p => p.LibraryItems)
            .HasForeignKey(d => d.LibraryBucketId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryItems_LibraryBucketId_LibraryBuckets_LibraryBucketId");

        builder.HasOne(d => d.Media)
            .WithMany(p => p.LibraryItems)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryItems_MediaId_Media_MediaId");

        builder.HasOne(d => d.Creator)
            .WithMany()
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryItems_CreatedBy_Users_UserId");

        builder.HasOne(d => d.Updater)
            .WithMany()
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibraryItems_UpdatedBy_Users_UserId");
    }
}
