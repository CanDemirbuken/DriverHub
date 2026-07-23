using DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IBannerQueryService
{
    Task<IReadOnlyList<GetAllBannerQueryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}