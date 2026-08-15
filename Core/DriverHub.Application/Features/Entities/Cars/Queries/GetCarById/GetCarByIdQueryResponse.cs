using DriverHub.Domain.Enums;
using static DriverHub.Application.Features.Entities.Cars.Queries.GetCarById.GetCarByIdQueryResponse;

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
    string BigImageUrl,
    IReadOnlyCollection<FeatureItem> Features,
    IReadOnlyCollection<PricingItem> Pricings
)
{
    public sealed record FeatureItem(
        Guid FeatureId,
        string FeatureName
    );

    public sealed record PricingItem(
        PricingType Type,
        decimal Amount
    );
}