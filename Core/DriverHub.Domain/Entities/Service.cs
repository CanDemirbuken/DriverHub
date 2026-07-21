using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Service : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
}