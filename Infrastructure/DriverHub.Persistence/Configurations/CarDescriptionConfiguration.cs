using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class CarDescriptionConfiguration : EntityConfiguration<CarDescription>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CarDescription> builder)
    {
        builder.ToTable("CarDescriptions");

        builder.Property(x => x.Details)
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(x => x.CarId)
            .IsRequired();

        builder.HasIndex(x => x.CarId)
            .IsUnique();
    }
}