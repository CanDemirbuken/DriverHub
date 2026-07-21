using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class SocialMedia : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}