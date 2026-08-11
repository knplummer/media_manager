using MediaManager.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class MediaHistoryConfiguration : IEntityTypeConfiguration<MediaHistory>
{
    public void Configure(EntityTypeBuilder<MediaHistory> builder)
    {
        builder.ToTable("MediaHistory");
        builder.HasKey(e => e.MediaHistoryId);

        builder.Property(e => e.StorageGuid).HasMaxLength(36);
        builder.Property(e => e.Action).HasMaxLength(10).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(200).IsRequired();
        builder.Property(e => e.ActionTime).IsRequired();

        builder.HasOne(d => d.Media)
            .WithMany(p => p.History)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("MediaHistory_MediaId_Media_MediaId");

        builder.HasOne(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.ActionUser)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("MediaHistory_ActionUser_Users_UserId");
    }
}
