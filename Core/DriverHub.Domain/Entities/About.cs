using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class About : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}