namespace DriverHub.WebApi.Contracts.Cars;

public sealed record SetCarPricingsRequest(
    IReadOnlyCollection<SetCarPricingRequestItem> Pricings
);