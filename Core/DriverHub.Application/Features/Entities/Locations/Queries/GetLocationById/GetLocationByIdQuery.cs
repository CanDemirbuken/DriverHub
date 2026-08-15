using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Queries.GetLocationById;

public sealed record GetLocationByIdQuery(
    Guid Id
) : IRequest<Result<GetLocationByIdQueryResponse>>;