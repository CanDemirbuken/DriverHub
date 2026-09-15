using DriverHub.Domain.Enums;

namespace DriverHub.Persistence.Communication;

public sealed class EmailOutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ReservationId { get; set; }
    public ReservationStatus Status { get; set; }
    public string Recipient { get; set; } = "";
    public string Subject { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime NextAttemptAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public int Attempts { get; set; }
}
