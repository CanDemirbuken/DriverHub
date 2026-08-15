using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using DriverHub.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class ReservationConfiguration : EntityConfiguration<Reservation>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations", tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "CK_Reservations_DateRange_Valid",
                "[EndDate] > [StartDate]");

            tableBuilder.HasCheckConstraint(
                "CK_Reservations_BasePrice_NonNegative",
                "[BasePrice] >= 0");

            tableBuilder.HasCheckConstraint(
                "CK_Reservations_ExtraPrice_NonNegative",
                "[ExtraPrice] >= 0");

            tableBuilder.HasCheckConstraint(
                "CK_Reservations_InsurancePrice_NonNegative",
                "[InsurancePrice] >= 0");

            tableBuilder.HasCheckConstraint(
                "CK_Reservations_TotalPrice_NonNegative",
                "[TotalPrice] >= 0");

            tableBuilder.HasCheckConstraint(
                "CK_Reservations_Status_Valid",
                "[Status] BETWEEN 1 AND 4");
        });

        builder.Property(x => x.UserId)
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(x => x.CarId)
            .IsRequired();

        builder.Property(x => x.PickupLocationId)
            .IsRequired();

        builder.Property(x => x.ReturnLocationId)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.BasePrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ExtraPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.InsurancePrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TotalPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.CarId,
            x.StartDate,
            x.EndDate,
            x.Status
        });

        builder.HasIndex(x => x.UserId);

        builder.HasOne(x => x.Car)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(x => x.PickupLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(x => x.ReturnLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.InsurancePackageId)
            .IsRequired(false);

        builder.HasOne(x => x.InsurancePackage)
            .WithMany()
            .HasForeignKey(x => x.InsurancePackageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}