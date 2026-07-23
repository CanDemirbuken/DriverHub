using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;

public sealed class GetAllBannerQueryHandler(IBannerQueryService bannerQueryService) : IRequestHandler<GetAllBannerQuery, IReadOnlyList<GetAllBannerQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllBannerQueryResponse>> Handle(GetAllBannerQuery request, CancellationToken cancellationToken)
    {
        return await bannerQueryService.GetAllAsync(cancellationToken);
    }
}