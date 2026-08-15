using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class ExtraConfiguration : EntityConfiguration<Extra>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Extra> builder)
    {
        builder.ToTable("Extras", tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "CK_Extras_DailyPrice_NonNegative",
                "[DailyPrice] >= 0");
        });

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DailyPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}