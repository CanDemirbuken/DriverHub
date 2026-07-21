using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class CarFeature : Entity
{
    public Guid CarId { get; set; }
    public Car? Car { get; set; }

    public Guid FeatureId { get; set; }
    public Feature? Feature { get; set; }

    public bool IsAvailable { get; set; }
}