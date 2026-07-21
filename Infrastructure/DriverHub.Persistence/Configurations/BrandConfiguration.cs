using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class BrandConfiguration : EntityConfiguration<Brand>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany(x => x.Cars)
            .WithOne(x => x.Brand)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}