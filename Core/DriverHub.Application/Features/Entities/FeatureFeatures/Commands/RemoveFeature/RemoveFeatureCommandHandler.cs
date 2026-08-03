using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.FeatureFeatures.Commands.RemoveFeature;

public sealed class RemoveFeatureCommandHandler(IRepository<Feature> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveFeatureCommand, Result>
{
    public async Task<Result> Handle(RemoveFeatureCommand request, CancellationToken cancellationToken)
    {
        var feature = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (feature is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        repository.Remove(feature);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}