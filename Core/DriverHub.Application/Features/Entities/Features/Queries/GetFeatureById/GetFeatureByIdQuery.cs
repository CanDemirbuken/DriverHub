using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Features.Queries.GetFeatureById;

public sealed record GetFeatureByIdQuery(
    Guid Id
) : IRequest<Result<GetFeatureByIdQueryResponse>>;