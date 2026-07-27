using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.FeatureFeatures.Commands.RemoveFeature;

public sealed class RemoveFeatureCommandHandler(IRepository<Feature> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveFeatureCommand, Result>
{
    public async Task<Result> Handle(RemoveFeatureCommand request, CancellationToken cancellationToken)
    {
        var feature = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (feature is null)
            return Result.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        repository.Remove(feature);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}