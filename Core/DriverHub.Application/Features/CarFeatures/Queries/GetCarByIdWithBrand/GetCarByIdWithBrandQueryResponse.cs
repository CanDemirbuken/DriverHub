namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed record GetCarByIdWithBrandQueryResponse(Guid Id, string BrandName, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, byte Luggage, string Fuel, string BigImageUrl);