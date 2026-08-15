using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Features.Commands.RemoveFeature;

public sealed class RemoveFeatureCommandHandler(IRepository<Feature> featureRepository, IRepository<CarFeature> carFeatureRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveFeatureCommand, Result>
{
    public async Task<Result> Handle(RemoveFeatureCommand request, CancellationToken cancellationToken)
    {
        Feature? feature = await featureRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (feature is null)
            return Result.Failure(
                Error.NotFound(
                    $"{request.Id} kimlik bilgisine sahip özellik bulunamadı.",
                    nameof(request.Id)));

        bool isInUse = await carFeatureRepository.AnyAsync(
            carFeature => carFeature.FeatureId == request.Id,
            cancellationToken);

        if (isInUse)
            return Result.Failure(
                Error.Conflict(
                    "Bu özellik araçlara atanmış olduğu için silinemez.",
                    nameof(request.Id)));

        featureRepository.Remove(feature);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}