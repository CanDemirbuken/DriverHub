using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.BannerFeatures.Commands.UpdateBanner;

public sealed class UpdateBannerCommandHandler(IRepository<Banner> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateBannerCommand, Result>
{
    public async Task<Result> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (banner is null)
            return Result.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        mapper.Map(request, banner);

        repository.Update(banner);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}