namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCars;

public sealed record GetPagedCarsQueryResponse(Guid Id, Guid BrandId, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, byte Luggage, string Fuel, string BigImageUrl);