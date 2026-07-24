using MediatR;

namespace DriverHub.Application.Features.FeatureFeatures.Queries.GetAllFeature;

public sealed record GetAllFeatureQuery : IRequest<IReadOnlyList<GetAllFeatureQueryResponse>>;