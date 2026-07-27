using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.FeatureFeatures.Commands.CreateFeature;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.FeatureFeatures.Commands.UpdateFeature;

public sealed class UpdateFeatureCommandHandler(IRepository<Feature> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateFeatureCommand, Result>
{
    public async Task<Result> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
    {
        var feature = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (feature is null)
            return Result.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        bool featureExist = await repository.AnyAsync(predicate: f => f.Id != request.Id && f.Name == request.Name, cancellationToken);
        if (featureExist)
            return Result<CreateFeatureCommandResponse>.Failure(StatusCodes.Status409Conflict, "Bu isme sahip bir feature zaten kayıtlı.");

        mapper.Map(request, feature);

        repository.Update(feature);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}
