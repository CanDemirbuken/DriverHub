using DriverHub.Domain.Abstraction;
using DriverHub.Domain.Enums;

public sealed class CarPricing : Entity
{
    public Guid CarId { get; set; }
    public Car? Car { get; set; }

    public PricingType Type { get; set; }

    public decimal Amount { get; set; }
}