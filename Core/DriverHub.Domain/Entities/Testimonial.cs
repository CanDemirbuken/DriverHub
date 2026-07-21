using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Testimonial : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}