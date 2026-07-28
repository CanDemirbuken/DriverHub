using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Commands.CreateBanner;

public sealed class CreateBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateBannerCommand, Result<CreateBannerCommandResponse>>
{
    public async Task<Result<CreateBannerCommandResponse>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        Banner banner = mapper.Map<Banner>(request);

        await repository.AddAsync(banner, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateBannerCommandResponse data = new CreateBannerCommandResponse(banner.Id);
        return Result<CreateBannerCommandResponse>.Success(data);
    }
}