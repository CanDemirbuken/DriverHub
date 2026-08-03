using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.BannerFeatures.Commands.UpdateBanner;

public sealed class UpdateBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateBannerCommand, Result>
{
    public async Task<Result> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (banner is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        mapper.Map(request, banner);

        repository.Update(banner);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}