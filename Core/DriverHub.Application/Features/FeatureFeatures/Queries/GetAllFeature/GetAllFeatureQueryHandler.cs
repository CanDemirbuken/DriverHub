using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.FeatureFeatures.Queries.GetAllFeature;

public sealed class GetAllFeatureQueryHandler(IFeatureQueryService featureQueryService) : IRequestHandler<GetAllFeatureQuery, IReadOnlyList<GetAllFeatureQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllFeatureQueryResponse>> Handle(GetAllFeatureQuery request, CancellationToken cancellationToken)
    {
        return await featureQueryService.GetAllAsync(cancellationToken);
    }
}