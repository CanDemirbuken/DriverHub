using DriverHub.Domain.Enums;

namespace DriverHub.Application.Features.Entities.Cars.Queries.GetPagedCars;

public sealed record GetPagedCarsQueryResponse(
    Guid Id,
    string CoverImageUrl,
    string Plate,
    string BrandName,
    string Model,
    short ModelYear,
    string CategoryName,
    string CurrentLocationName,
    int Km,
    string Transmission,
    string Fuel,
    CarStatus Status
);