using DriverHub.Domain.Enums;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;

public sealed record GetAvailableCarsQueryResponse(
    Guid Id,
    string CoverImageUrl,
    string BrandName,
    string Model,
    short ModelYear,
    string Plate,
    string CategoryName,
    string CurrentLocationName,
    int Km,
    string Transmission,
    string Fuel,
    CarStatus Status);
