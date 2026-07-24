namespace DriverHub.WebApi.Models.Cars;

public sealed record UpdateCarRequest(Guid BrandId, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, byte Luggage, string Fuel, string BigImageUrl);