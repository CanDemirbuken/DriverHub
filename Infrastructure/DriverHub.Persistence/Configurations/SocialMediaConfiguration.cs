using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class SocialMediaConfiguration : EntityConfiguration<SocialMedia>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SocialMedia> builder)
    {
        builder.ToTable("SocialMedias");

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Url)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Icon)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasIndex(x => x.Url)
            .IsUnique();
    }
}