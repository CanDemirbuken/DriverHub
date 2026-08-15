using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class ReservationExtraConfiguration : EntityConfiguration<ReservationExtra>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ReservationExtra> builder)
    {
        builder.ToTable("ReservationExtras", tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "CK_ReservationExtras_UnitPrice_NonNegative",
                "[UnitPrice] >= 0");

            tableBuilder.HasCheckConstraint(
                "CK_ReservationExtras_TotalPrice_NonNegative",
                "[TotalPrice] >= 0");
        });

        builder.Property(x => x.ReservationId)
            .IsRequired();

        builder.Property(x => x.ExtraId)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TotalPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.ReservationId,
            x.ExtraId
        }).IsUnique();

        builder.HasOne(x => x.Reservation)
            .WithMany(x => x.ReservationExtras)
            .HasForeignKey(x => x.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Extra)
            .WithMany(x => x.ReservationExtras)
            .HasForeignKey(x => x.ExtraId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}