using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.CarFeatures.Commands.RemoveCar;

public sealed class RemoveCarCommandHandler(IRepository<Car> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveCarCommand, Result>
{
    public async Task<Result> Handle(RemoveCarCommand request, CancellationToken cancellationToken)
    {
        var car = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            return Result.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        repository.Remove(car);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}