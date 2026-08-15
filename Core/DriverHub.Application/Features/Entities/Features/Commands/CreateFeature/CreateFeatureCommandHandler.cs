using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Features.Commands.CreateFeature;

public sealed class CreateFeatureCommandHandler(IRepository<Feature> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateFeatureCommand, Result<CreateFeatureCommandResponse>>
{
    public async Task<Result<CreateFeatureCommandResponse>> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
    {
        bool featureExists = await repository.AnyAsync(
            feature => feature.Name == request.Name,
            cancellationToken);

        if (featureExists)
            return Result<CreateFeatureCommandResponse>.Failure(
                Error.Conflict(
                    "Bu özellik adına sahip bir kayıt zaten mevcut.",
                    nameof(request.Name)));

        Feature feature = mapper.Map<Feature>(request);

        await repository.AddAsync(feature, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateFeatureCommandResponse data = new CreateFeatureCommandResponse(feature.Id);

        return Result<CreateFeatureCommandResponse>.Success(data);
    }
}