namespace DriverHub.Application.Features.CarFeatures.Queries.GetAllCar;

public sealed record GetAllCarQueryResponse(Guid Id, Guid BrandId, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, byte Luggage, string Fuel, string BigImageUrl);