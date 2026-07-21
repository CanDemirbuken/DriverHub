using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class CarDescription : Entity
{
    public string Details { get; set; } = string.Empty;
    public Guid CarId { get; set; }
    public Car? Car { get; set; }
}