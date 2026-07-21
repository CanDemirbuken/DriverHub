using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class FooterAddress : Entity
{
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}