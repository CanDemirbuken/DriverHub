using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class CarConfiguration : EntityConfiguration<Car>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars", tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "CK_Cars_Km_NonNegative",
                "[Km] >= 0");

            tableBuilder.HasCheckConstraint(
                "CK_Cars_Seat_Range",
                "[Seat] BETWEEN 1 AND 9");

            tableBuilder.HasCheckConstraint(
                "CK_Cars_Luggage_Positive",
                "[Luggage] > 0");
        });

        builder.Property(x => x.Model)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.CoverImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Km)
            .IsRequired();

        builder.Property(x => x.Transmission)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Seat)
            .IsRequired();

        builder.Property(x => x.Luggage)
            .IsRequired();

        builder.Property(x => x.Fuel)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.BigImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.BrandId,
            x.Model
        }).IsUnique();

        builder.HasOne(x => x.Brand)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CarDescription)
            .WithOne(x => x.Car)
            .HasForeignKey<CarDescription>(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CarFeatures)
            .WithOne(x => x.Car)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CarPricings)
            .WithOne(x => x.Car)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}