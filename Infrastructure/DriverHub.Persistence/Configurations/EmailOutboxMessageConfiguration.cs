using DriverHub.Domain.Entities;
using DriverHub.Persistence.Communication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Configurations;

public sealed class EmailOutboxMessageConfiguration : IEntityTypeConfiguration<EmailOutboxMessage>
{
    public void Configure(EntityTypeBuilder<EmailOutboxMessage> builder)
    {
        builder.ToTable("EmailOutboxMessages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Recipient).HasMaxLength(256);
        builder.Property(x => x.Subject).HasMaxLength(200);
        builder.HasIndex(x => new { x.ReservationId, x.Status }).IsUnique();
        builder.HasIndex(x => new { x.NextAttemptAt, x.CreatedAt }).HasFilter("[DeliveredAt] IS NULL");
        builder.HasOne<Reservation>().WithMany().HasForeignKey(x => x.ReservationId).OnDelete(DeleteBehavior.Restrict);
    }
}
