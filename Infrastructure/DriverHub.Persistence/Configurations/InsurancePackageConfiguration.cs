using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class InsurancePackageConfiguration : EntityConfiguration<InsurancePackage>
{
    protected override void ConfigureEntity(EntityTypeBuilder<InsurancePackage> builder)
    {
        builder.ToTable("InsurancePackages", tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "CK_InsurancePackages_DailyPrice_NonNegative",
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