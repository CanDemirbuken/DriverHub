using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.BannerFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.BannerFeatures.Commands.CreateBanner;

public sealed class CreateBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBannerCommand, Result<CreateBannerCommandResponse>>
{
    public async Task<Result<CreateBannerCommandResponse>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        Banner banner = request.ToEntity();

        await repository.AddAsync(banner, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateBannerCommandResponse data = new CreateBannerCommandResponse(banner.Id);
        return Result<CreateBannerCommandResponse>.Success(data, StatusCodes.Status201Created);
    }
}