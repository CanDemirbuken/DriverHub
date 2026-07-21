using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class ContactConfiguration : EntityConfiguration<Contact>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(254)
            .IsRequired();

        builder.Property(x => x.Subject)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Message)
            .HasMaxLength(4000)
            .IsRequired();

        builder.HasIndex(x => x.Email);
    }
}