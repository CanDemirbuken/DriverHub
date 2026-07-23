namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarById;

public sealed record GetCarByIdQueryResponse(Guid Id, Guid BrandId, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, byte Luggage, string Fuel, string BigImageUrl);