using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.FeatureFeatures.Commands.CreateFeature
{
    public sealed class CreateFeatureCommandHandler(IRepository<Feature> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateFeatureCommand, Result<CreateFeatureCommandResponse>>
    {
        public async Task<Result<CreateFeatureCommandResponse>> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            Feature feature = mapper.Map<Feature>(request);

            bool featureExist = await repository.AnyAsync(predicate: f => f.Name == request.Name, cancellationToken);
            if (featureExist)
                return Result<CreateFeatureCommandResponse>.Failure(Error.Conflict("Bu isme sahip bir feature zaten kayıtlı.", nameof(request.Name)));

            await repository.AddAsync(feature, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var data = new CreateFeatureCommandResponse(feature.Id);
            return Result<CreateFeatureCommandResponse>.Success(data);
        }
    }
}