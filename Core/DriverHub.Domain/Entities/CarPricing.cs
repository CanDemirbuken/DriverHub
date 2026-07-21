using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class CarPricing : Entity
{
    public Guid CarId { get; set; }
    public Car? Car { get; set; }
    public Guid PricingId { get; set; }
    public Pricing? Pricing { get; set; }
    public decimal Amount { get; set; }
}