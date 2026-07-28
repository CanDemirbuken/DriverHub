using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetBannerById;

public sealed class GetBannerByIdQueryHandler(IRepository<Banner> repository, IMapper mapper) : IRequestHandler<GetBannerByIdQuery, Result<GetBannerByIdQueryResponse>>
{
    public async Task<Result<GetBannerByIdQueryResponse>> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
    {
        var banner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (banner is null)
            return Result<GetBannerByIdQueryResponse>.Failure(Error.NotFound($"{request.Id} kimliğine sahip kayıt bulunamadı.", nameof(request.Id)));

        GetBannerByIdQueryResponse data = mapper.Map<GetBannerByIdQueryResponse>(banner);
        return Result<GetBannerByIdQueryResponse>.Success(data);
    }
}