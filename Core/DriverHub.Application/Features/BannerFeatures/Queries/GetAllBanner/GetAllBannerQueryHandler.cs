using DriverHub.Application.Features.BannerFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;

public sealed class GetAllBannerQueryHandler(IRepository<Banner> repository) : IRequestHandler<GetAllBannerQuery, IReadOnlyList<GetAllBannerQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllBannerQueryResponse>> Handle(GetAllBannerQuery request, CancellationToken cancellationToken)
    {
        var banners = await repository.GetAllAsync(cancellationToken);

        IReadOnlyList<GetAllBannerQueryResponse> response = banners.Select(b => b.ToGetAllResponse()).ToList();
        return response;
    }
}