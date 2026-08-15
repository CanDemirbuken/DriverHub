using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class CarPricingConfiguration : EntityConfiguration<CarPricing>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CarPricing> builder)
    {
        builder.ToTable("CarPricings", tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "CK_CarPricings_Amount_Positive",
                "[Amount] > 0");

            tableBuilder.HasCheckConstraint(
                "CK_CarPricings_Type_Valid",
                "[Type] BETWEEN 1 AND 3");
        });

        builder.Property(x => x.CarId)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.CarId,
            x.Type
        }).IsUnique();

        builder.HasOne(x => x.Car)
            .WithMany(x => x.CarPricings)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}