using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.Entities.FeatureFeatures.Queries.GetAllFeature;

public sealed class GetAllFeatureQueryHandler(IFeatureQueryService featureQueryService) : IRequestHandler<GetAllFeatureQuery, Result<IReadOnlyList<GetAllFeatureQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllFeatureQueryResponse>>> Handle(GetAllFeatureQuery request, CancellationToken cancellationToken)
    {
        var data = await featureQueryService.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllFeatureQueryResponse>>.Success(data);
    }
}