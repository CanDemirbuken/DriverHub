using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; set; } = string.Empty;
}
