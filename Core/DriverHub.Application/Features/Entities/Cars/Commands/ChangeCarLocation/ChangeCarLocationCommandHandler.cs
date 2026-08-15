using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.ChangeCarLocation;

public sealed class ChangeCarLocationCommandHandler(IRepository<Car> carRepository, IRepository<Location> locationRepository, IUnitOfWork unitOfWork) : IRequestHandler<ChangeCarLocationCommand, Result>
{
    public async Task<Result> Handle(ChangeCarLocationCommand request, CancellationToken cancellationToken)
    {
        Car? car = await carRepository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip araç bulunamadı.", nameof(request.Id)));

        bool locationExists = await locationRepository.AnyAsync(
            location => location.Id == request.CurrentLocationId,
            cancellationToken);

        if (!locationExists)
            return Result.Failure(Error.NotFound("Lokasyon bilgisi bulunamadı.", nameof(request.CurrentLocationId)));

        if (car.CurrentLocationId == request.CurrentLocationId)
            return Result.Success();

        car.CurrentLocationId = request.CurrentLocationId;

        carRepository.Update(car);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}