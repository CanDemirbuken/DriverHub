using DriverHub.Application.Features.Entities.Features.Queries.GetAllFeature;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IFeatureQueryService
{
    Task<IReadOnlyList<GetAllFeatureQueryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}