using DriverHub.Domain.Abstraction;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;

public sealed class Car : Entity
{
    public Guid BrandId { get; set; }
    public Brand? Brand { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public Guid CurrentLocationId { get; set; }
    public Location? CurrentLocation { get; set; }

    public string Model { get; set; } = string.Empty;
    public short ModelYear { get; set; }

    public string Plate { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;

    public int Km { get; set; }
    public string Transmission { get; set; } = string.Empty;
    public string Fuel { get; set; } = string.Empty;

    public byte Seat { get; set; }
    public int Luggage { get; set; }

    public string Color { get; set; } = string.Empty;

    public CarStatus Status { get; set; }

    public string CoverImageUrl { get; set; } = string.Empty;
    public string BigImageUrl { get; set; } = string.Empty;

    public ICollection<CarFeature> CarFeatures { get; set; } = [];
    public CarDescription? CarDescription { get; set; }
    public ICollection<CarPricing> CarPricings { get; set; } = [];
    public ICollection<Reservation> Reservations { get; set; } = [];
}