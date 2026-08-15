using DriverHub.Domain.Enums;

namespace DriverHub.Application.Features.Entities.Cars.Commands.SetCarPricings;

public sealed record SetCarPricingItem(
    PricingType Type,
    decimal Amount
);