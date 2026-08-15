using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class InsurancePackage : Entity
{
    public string Name { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
}