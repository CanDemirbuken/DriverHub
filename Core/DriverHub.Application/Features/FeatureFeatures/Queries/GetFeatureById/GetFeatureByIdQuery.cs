using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.FeatureFeatures.Queries.GetFeatureById;

public sealed record GetFeatureByIdQuery(Guid Id) : IRequest<Result<GetFeatureByIdQueryResponse>>;