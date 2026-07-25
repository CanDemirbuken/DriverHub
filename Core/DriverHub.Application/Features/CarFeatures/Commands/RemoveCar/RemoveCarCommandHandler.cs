using DriverHub.Application.Exceptions;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Commands.RemoveCar;

public sealed class RemoveCarCommandHandler(IRepository<Car> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveCarCommand>
{
    public async Task Handle(RemoveCarCommand request, CancellationToken cancellationToken)
    {
        var car = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            throw new NotFoundException();

        repository.Remove(car);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}