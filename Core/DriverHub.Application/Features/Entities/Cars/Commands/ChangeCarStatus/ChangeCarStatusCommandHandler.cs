using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.ChangeCarStatus;

public sealed class ChangeCarStatusCommandHandler(IRepository<Car> carRepository, IUnitOfWork unitOfWork) : IRequestHandler<ChangeCarStatusCommand, Result>
{
    public async Task<Result> Handle(ChangeCarStatusCommand request, CancellationToken cancellationToken)
    {
        Car? car = await carRepository.GetByIdAsync(request.Id, cancellationToken);

        if (car is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip araç bulunamadı.", nameof(request.Id)));

        if (car.Status == request.Status)
            return Result.Success();

        car.Status = request.Status;

        carRepository.Update(car);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}