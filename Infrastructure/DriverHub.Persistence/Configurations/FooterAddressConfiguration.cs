using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class FooterAddressConfiguration : EntityConfiguration<FooterAddress>
{
    protected override void ConfigureEntity(EntityTypeBuilder<FooterAddress> builder)
    {
        builder.ToTable("FooterAddresses");

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(254)
            .IsRequired();
    }
}