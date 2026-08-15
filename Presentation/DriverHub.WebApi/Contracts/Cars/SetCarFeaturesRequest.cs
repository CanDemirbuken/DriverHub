namespace DriverHub.WebApi.Contracts.Cars;

public sealed record SetCarFeaturesRequest(IReadOnlyCollection<Guid> FeatureIds);