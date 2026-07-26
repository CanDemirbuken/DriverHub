using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;

public sealed class GetAllBannerQueryHandler(IBannerQueryService bannerQueryService) : IRequestHandler<GetAllBannerQuery, Result<IReadOnlyList<GetAllBannerQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllBannerQueryResponse>>> Handle(GetAllBannerQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<GetAllBannerQueryResponse> data = await bannerQueryService.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllBannerQueryResponse>>.Success(data, StatusCodes.Status200OK);
    }
}