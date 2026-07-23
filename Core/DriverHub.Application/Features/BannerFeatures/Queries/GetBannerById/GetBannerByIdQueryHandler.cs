using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.BannerFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetBannerById;

public sealed class GetBannerByIdQueryHandler(IRepository<Banner> repository) : IRequestHandler<GetBannerByIdQuery, GetBannerByIdQueryResponse>
{
    public async Task<GetBannerByIdQueryResponse> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
    {
        var banner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (banner is null)
            throw new NotFoundException();

        GetBannerByIdQueryResponse response = banner.ToGetByIdResponse();
        return response;
    }
}