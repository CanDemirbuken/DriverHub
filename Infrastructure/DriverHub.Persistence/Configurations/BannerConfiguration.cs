using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class BannerConfiguration : EntityConfiguration<Banner>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Banner> builder)
    {
        builder.ToTable("Banners");

        builder.Property(x => x.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.VideoDescription)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.VideoUrl)
            .HasMaxLength(500)
            .IsRequired();
    }
}