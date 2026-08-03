using DriverHub.Application.Features.Entities.BannerFeatures.Queries.GetAllBanner;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IBannerQueryService
{
    Task<IReadOnlyList<GetAllBannerQueryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}