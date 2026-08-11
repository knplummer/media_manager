using MediaManager.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class LibrarySubItemConfiguration : IEntityTypeConfiguration<LibrarySubItem>
{
    public void Configure(EntityTypeBuilder<LibrarySubItem> builder)
    {
        builder.ToTable("LibrarySubItems");
        builder.HasKey(e => e.LibrarySubItemId);

        builder.Property(e => e.Type).HasMaxLength(25).IsRequired();
        builder.Property(e => e.OwnedFormat).HasMaxLength(15).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Metadata).HasMaxLength(750).IsRequired();

        builder.HasOne(d => d.LibraryItem)
            .WithMany(p => p.LibrarySubItems)
            .HasForeignKey(d => d.LibraryItemId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibrarySubItems_LibraryItemId_LibraryItems_LibraryItemId");

        builder.HasOne(d => d.Media)
            .WithMany(p => p.LibrarySubItems)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibrarySubItems_MediaId_Media_MediaId");

        builder.HasOne(d => d.Creator)
            .WithMany()
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibrarySubItems_CreatedBy_Users_UserId");

        builder.HasOne(d => d.Updater)
            .WithMany()
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("LibrarySubItems_UpdatedBy_Users_UserId");
    }
}
