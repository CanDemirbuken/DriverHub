using DriverHub.Domain.Enums;

namespace DriverHub.WebApi.Contracts.Cars;

public sealed record SetCarPricingRequestItem(
    PricingType Type,
    decimal Amount
);