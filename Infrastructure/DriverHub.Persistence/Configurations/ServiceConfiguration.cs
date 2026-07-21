using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class ServiceConfiguration : EntityConfiguration<Service>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");

        builder.Property(x => x.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.IconUrl)
            .HasMaxLength(500)
            .IsRequired();
    }
}