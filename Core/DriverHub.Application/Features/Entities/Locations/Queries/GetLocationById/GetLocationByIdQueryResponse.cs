namespace DriverHub.Application.Features.Entities.Locations.Queries.GetLocationById;

public sealed record GetLocationByIdQueryResponse(
    Guid Id,
    string Name
);