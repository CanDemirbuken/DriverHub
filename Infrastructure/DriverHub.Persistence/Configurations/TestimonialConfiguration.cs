using DriverHub.Domain.Entities;
using DriverHub.Persistence.Configurations.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class TestimonialConfiguration : EntityConfiguration<Testimonial>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Testimonial> builder)
    {
        builder.ToTable("Testimonials");

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500)
            .IsRequired();
    }
}