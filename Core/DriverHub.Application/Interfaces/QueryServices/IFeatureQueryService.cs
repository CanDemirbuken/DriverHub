using DriverHub.Application.Features.Entities.FeatureFeatures.Queries.GetAllFeature;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IFeatureQueryService
{
    Task<IReadOnlyList<GetAllFeatureQueryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}