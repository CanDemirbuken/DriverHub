using DriverHub.Domain.Abstraction;

namespace DriverHub.Domain.Entities;

public sealed class Car : Entity
{
    public Guid BrandId { get; set; }
    public Brand? Brand { get; set; }
    public string Model { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public int Km { get; set; }
    public string Transmission { get; set; } = string.Empty;
    public byte Seat { get; set; }
    public int Luggage { get; set; }
    public string Fuel { get; set; } = string.Empty;
    public string BigImageUrl { get; set; } = string.Empty;
    public ICollection<CarFeature> CarFeatures { get; set; } = [];
    public CarDescription? CarDescription { get; set; }
    public ICollection<CarPricing> CarPricings { get; set; } = [];
}