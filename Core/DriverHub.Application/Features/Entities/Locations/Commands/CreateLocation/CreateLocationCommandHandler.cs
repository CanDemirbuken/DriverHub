using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Commands.CreateLocation;

public sealed class CreateLocationCommandHandler(IRepository<Location> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateLocationCommand, Result<CreateLocationCommandResponse>>
{
    public async Task<Result<CreateLocationCommandResponse>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        bool locationExists = await repository.AnyAsync(
            location => location.Name == request.Name,
            cancellationToken);

        if (locationExists)
            return Result<CreateLocationCommandResponse>.Failure(
                Error.Conflict("Bu lokasyon adına sahip bir kayıt zaten mevcut.", nameof(request.Name)));


        Location location = mapper.Map<Location>(request);

        await repository.AddAsync(location, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateLocationCommandResponse data = new CreateLocationCommandResponse(location.Id);
        return Result<CreateLocationCommandResponse>.Success(data);
    }
}
