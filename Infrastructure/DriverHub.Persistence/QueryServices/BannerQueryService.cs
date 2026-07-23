using DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class BannerQueryService(AppDbContext context) : IBannerQueryService
{
    public async Task<IReadOnlyList<GetAllBannerQueryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Banner>()
            .AsNoTracking()
            .OrderByDescending(banner => banner.CreatedDate)
            .ThenByDescending(banner => banner.Id)
            .Select(banner => new GetAllBannerQueryResponse(
                banner.Id,
                banner.Title,
                banner.Description.Length > 50
                    ? banner.Description.Substring(0, 50) + "..."
                    : banner.Description))
            .ToListAsync(cancellationToken);
    }
}