namespace DriverHub.Application.Features.CarFeatures.Queries.GetAllCarWithBrand;

public sealed record GetAllCarWithBrandQueryResponse(Guid Id, string BrandName, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, byte Luggage, string Fuel, string BigImageUrl);