using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.BannerFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetBannerById;

public sealed class GetBannerByIdQueryHandler(IRepository<Banner> repository) : IRequestHandler<GetBannerByIdQuery, Result<GetBannerByIdQueryResponse>>
{
    public async Task<Result<GetBannerByIdQueryResponse>> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
    {

        var banner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (banner is null)
            return Result<GetBannerByIdQueryResponse>.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimliğine sahip kayıt bulunamadı.");

        GetBannerByIdQueryResponse data = banner.ToGetByIdResponse();
        return Result<GetBannerByIdQueryResponse>.Success(data, StatusCodes.Status200OK);
    }
}