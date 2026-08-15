using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.SetCarFeatures;

public sealed class SetCarFeaturesCommandHandler(IRepository<Car> carRepository, IRepository<Feature> featureRepository, IRepository<CarFeature> carFeatureRepository, IUnitOfWork unitOfWork) : IRequestHandler<SetCarFeaturesCommand, Result>
{
    public async Task<Result> Handle(SetCarFeaturesCommand request, CancellationToken cancellationToken)
    {
        Car? car = await carRepository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip araç bulunamadı.", nameof(request.Id)));

        Guid[] featureIds = request.FeatureIds
            .Distinct()
            .ToArray();

        if (featureIds.Length > 0)
        {
            IReadOnlyList<Feature> features = await featureRepository.WhereAsync(
                feature => featureIds.Contains(feature.Id),
                cancellationToken);

            if (features.Count != featureIds.Length)
                return Result.Failure(Error.NotFound("Gönderilen feature bilgilerinden biri veya birkaçı bulunamadı.", nameof(request.FeatureIds)));
        }

        IReadOnlyList<CarFeature> existingCarFeatures = await carFeatureRepository.WhereAsync(
            carFeature => carFeature.CarId == request.Id,
            cancellationToken);

        CarFeature[] carFeaturesToRemove = existingCarFeatures
            .Where(carFeature => !featureIds.Contains(carFeature.FeatureId))
            .ToArray();

        Guid[] existingFeatureIds = existingCarFeatures
            .Select(carFeature => carFeature.FeatureId)
            .ToArray();

        CarFeature[] carFeaturesToAdd = featureIds
            .Where(featureId => !existingFeatureIds.Contains(featureId))
            .Select(featureId => new CarFeature
            {
                CarId = request.Id,
                FeatureId = featureId
            })
            .ToArray();

        if (carFeaturesToRemove.Length > 0)
            carFeatureRepository.RemoveRange(carFeaturesToRemove);

        if (carFeaturesToAdd.Length > 0)
            await carFeatureRepository.AddRangeAsync(carFeaturesToAdd, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}