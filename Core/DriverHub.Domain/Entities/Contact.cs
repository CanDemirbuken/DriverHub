using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Contact : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}