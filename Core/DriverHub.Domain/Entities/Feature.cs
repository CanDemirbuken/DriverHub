using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Feature : Entity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<CarFeature> CarFeatures { get; set; } = [];
}