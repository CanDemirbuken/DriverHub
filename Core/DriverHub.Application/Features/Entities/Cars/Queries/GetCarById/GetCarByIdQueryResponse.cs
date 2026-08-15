using DriverHub.Domain.Enums;

namespace DriverHub.Application.Features.Entities.Cars.Queries.GetCarById;

public sealed record GetCarByIdQueryResponse(
    Guid Id,
    Guid BrandId,
    string BrandName,
    Guid CategoryId,
    string CategoryName,
    Guid CurrentLocationId,
    string CurrentLocationName,
    string Model,
    short ModelYear,
    string Plate,
    string Vin,
    string CoverImageUrl,
    int Km,
    string Transmission,
    byte Seat,
    int Luggage,
    string Fuel,
    string Color,
    CarStatus Status,
    string BigImageUrl
);