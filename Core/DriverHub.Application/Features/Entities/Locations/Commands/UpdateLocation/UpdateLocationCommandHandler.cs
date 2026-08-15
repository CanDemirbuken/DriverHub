using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Commands.UpdateLocation;

public sealed class UpdateLocationCommandHandler(IRepository<Location> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateLocationCommand, Result>
{
    public async Task<Result> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        Location? location = await repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (location is null)
            return Result.Failure(
                Error.NotFound(
                    $"{request.Id} kimlik bilgisine sahip lokasyon bulunamadı.",
                    nameof(request.Id)));

        bool locationExists = await repository.AnyAsync(
            location => location.Id != request.Id && location.Name == request.Name,
            cancellationToken);

        if (locationExists)
            return Result.Failure(
                Error.Conflict(
                    "Bu lokasyon adına sahip bir kayıt zaten mevcut.",
                    nameof(request.Name)));

        mapper.Map(request, location);

        repository.Update(location);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}