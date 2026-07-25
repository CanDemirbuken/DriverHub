using DriverHub.Application.Features.BannerFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Commands.CreateBanner;

public sealed class CreateBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBannerCommand, CreateBannerCommandResponse>
{
    public async Task<CreateBannerCommandResponse> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        Banner banner = request.ToEntity();

        await repository.AddAsync(banner, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateBannerCommandResponse response = new CreateBannerCommandResponse(banner.Id);
        return response;
    }
}