using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;

public sealed class GetAllBannerQueryHandler(IRepository<Banner> repository) : IRequestHandler<GetAllBannerQuery, IReadOnlyList<GetAllBannerQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllBannerQueryResponse>> Handle(GetAllBannerQuery request, CancellationToken cancellationToken)
    {
        var banners = await repository.GetAllAsync(cancellationToken);
        IReadOnlyList<GetAllBannerQueryResponse> response = banners.Select(b => new GetAllBannerQueryResponse
        (
            b.Id,
            b.Title,
            b.Description,
            b.VideoDescription,
            b.VideoUrl)
        ).ToList();

        return response;
    }
}