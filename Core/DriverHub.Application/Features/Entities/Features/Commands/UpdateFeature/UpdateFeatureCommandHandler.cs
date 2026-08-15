using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Features.Commands.UpdateFeature;

public sealed class UpdateFeatureCommandHandler(IRepository<Feature> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateFeatureCommand, Result>
{
    public async Task<Result> Handle(
        UpdateFeatureCommand request,
        CancellationToken cancellationToken)
    {
        Feature? feature = await repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (feature is null)
            return Result.Failure(
                Error.NotFound(
                    $"{request.Id} kimlik bilgisine sahip özellik bulunamadı.",
                    nameof(request.Id)));

        bool featureExists = await repository.AnyAsync(
            feature => feature.Id != request.Id &&
                       feature.Name == request.Name,
            cancellationToken);

        if (featureExists)
            return Result.Failure(
                Error.Conflict(
                    "Bu özellik adına sahip bir kayıt zaten mevcut.",
                    nameof(request.Name)));

        mapper.Map(request, feature);

        repository.Update(feature);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}