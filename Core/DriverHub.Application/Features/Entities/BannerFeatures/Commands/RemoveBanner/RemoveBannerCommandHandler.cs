using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.BannerFeatures.Commands.RemoveBanner;

public sealed class RemoveBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveBannerCommand, Result>
{
    public async Task<Result> Handle(RemoveBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (banner is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        repository.Remove(banner);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}