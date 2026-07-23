using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.BannerFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Commands.UpdateBanner;

public sealed class UpdateBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateBannerCommand>
{
    public async Task Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (banner is null)
            throw new NotFoundException();

        request.ApplyTo(banner);

        repository.Update(banner);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}