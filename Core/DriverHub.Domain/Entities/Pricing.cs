using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Pricing : Entity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<CarPricing> CarPricings { get; set; } = [];
}