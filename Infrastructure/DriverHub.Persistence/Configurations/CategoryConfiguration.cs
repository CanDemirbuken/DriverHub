using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class CategoryConfiguration : EntityConfiguration<Category>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}