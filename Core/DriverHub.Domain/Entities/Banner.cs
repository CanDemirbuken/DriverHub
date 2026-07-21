using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Banner : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string VideoDescription { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
}