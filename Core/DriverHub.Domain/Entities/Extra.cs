using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Extra : Entity
{
    public string Name { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }

    public ICollection<ReservationExtra> ReservationExtras { get; set; } = [];
}