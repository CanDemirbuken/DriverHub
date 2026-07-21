using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class CarFeatureConfiguration : EntityConfiguration<CarFeature>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CarFeature> builder)
    {
        builder.ToTable("CarFeatures");

        builder.Property(x => x.CarId)
            .IsRequired();

        builder.Property(x => x.FeatureId)
            .IsRequired();

        builder.Property(x => x.IsAvailable)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.CarId,
            x.FeatureId
        }).IsUnique();

        builder.HasOne(x => x.Feature)
            .WithMany(x => x.CarFeatures)
            .HasForeignKey(x => x.FeatureId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}