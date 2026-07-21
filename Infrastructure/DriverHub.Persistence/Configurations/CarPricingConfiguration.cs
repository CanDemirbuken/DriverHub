using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class CarPricingConfiguration : EntityConfiguration<CarPricing>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CarPricing> builder)
    {
        builder.ToTable("CarPricings");

        builder.Property(x => x.CarId)
            .IsRequired();

        builder.Property(x => x.PricingId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.CarId,
            x.PricingId
        }).IsUnique();

        builder.HasOne(x => x.Pricing)
            .WithMany(x => x.CarPricings)
            .HasForeignKey(x => x.PricingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}