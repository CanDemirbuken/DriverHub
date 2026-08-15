using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Commands.RemoveLocation;

public sealed class RemoveLocationCommandHandler(IRepository<Location> locationRepository, IRepository<Car> carRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveLocationCommand, Result>
{
    public async Task<Result> Handle(RemoveLocationCommand request, CancellationToken cancellationToken)
    {
        Location? location = await locationRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (location is null)
            return Result.Failure(
                Error.NotFound(
                    $"{request.Id} kimlik bilgisine sahip lokasyon bulunamadı.",
                    nameof(request.Id)));

        bool hasCars = await carRepository.AnyAsync(
            car => car.CurrentLocationId == request.Id,
            cancellationToken);

        if (hasCars)
            return Result.Failure(
                Error.Conflict(
                    "Bu lokasyona bağlı araçlar bulunduğu için lokasyon silinemez.",
                    nameof(request.Id)));

        locationRepository.Remove(location);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}