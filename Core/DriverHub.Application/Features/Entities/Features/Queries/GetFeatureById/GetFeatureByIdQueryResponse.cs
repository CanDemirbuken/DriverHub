namespace DriverHub.Application.Features.Entities.Features.Queries.GetFeatureById;

public sealed record GetFeatureByIdQueryResponse(
    Guid Id,
    string Name
);