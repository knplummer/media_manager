using MediaManager.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable("Media");
        builder.HasKey(e => e.MediaId);

        builder.Property(e => e.Type).HasMaxLength(5).IsRequired();
        builder.Property(e => e.Path).HasMaxLength(250).IsRequired();
        builder.Property(e => e.FileName).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Extension).HasMaxLength(5).IsRequired();
        builder.Property(e => e.FileSize).IsRequired();
        builder.Property(e => e.IsAlwaysAvailable).IsRequired();
        builder.Property(e => e.IsOnMediaServer).IsRequired();
        builder.Property(e => e.ExternalStorageKey).HasMaxLength(15);
        builder.Property(e => e.StorageGuid).HasMaxLength(36);

        builder.HasOne(d => d.Creator)
            .WithMany()
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("Media_CreatedBy_Users_UserId");

        builder.HasOne(d => d.Updater)
            .WithMany()
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("Media_UpdatedBy_Users_UserId");
    }
}
