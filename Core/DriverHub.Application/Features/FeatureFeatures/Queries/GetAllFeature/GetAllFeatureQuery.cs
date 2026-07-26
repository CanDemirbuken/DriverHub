using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.FeatureFeatures.Queries.GetAllFeature;

public sealed record GetAllFeatureQuery : IRequest<Result<IReadOnlyList<GetAllFeatureQueryResponse>>>;