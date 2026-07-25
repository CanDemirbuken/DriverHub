using DriverHub.Application.Exceptions;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Commands.RemoveBanner;

public sealed class RemoveBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveBannerCommand>
{
    public async Task Handle(RemoveBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (banner is null)
            throw new NotFoundException();

        repository.Remove(banner);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
