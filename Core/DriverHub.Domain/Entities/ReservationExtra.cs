using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class ReservationExtra : Entity
{
    public Guid ReservationId { get; set; }
    public Reservation? Reservation { get; set; }

    public Guid ExtraId { get; set; }
    public Extra? Extra { get; set; }

    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}