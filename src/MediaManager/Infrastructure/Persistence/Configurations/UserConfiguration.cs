using MediaManager.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(e => e.UserId);

        builder.Property(e => e.Username)
            .HasMaxLength(25)
            .IsRequired();
        
        builder.HasIndex(e => e.Username).IsUnique();

        builder.Property(e => e.IsActive).IsRequired();
        builder.Property(e => e.LastLogin).IsRequired();
    }
}
