namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;

public record GetPagedCarsWithBrandQueryResponse(Guid Id, string BrandName, string Model, int Km, string Transmission, string Fuel);