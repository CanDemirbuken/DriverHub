using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.CarFeatures.Commands.UpdateCar;

public sealed class UpdateCarCommandHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCarCommand, Result>
{
    public async Task<Result> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            return Result.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        bool brandExists = await brandRepository.AnyAsync(b => b.Id == request.BrandId, cancellationToken);
        if (!brandExists)
            return Result.Failure(StatusCodes.Status404NotFound, "Marka bilgisi bulunamadı.");

        bool carExists = await carRepository.AnyAsync(predicate: c => c.Id != request.Id && c.Model == request.Model && c.BrandId == request.BrandId, cancellationToken);
        if (carExists)
            return Result.Failure(StatusCodes.Status409Conflict, "Bu marka ve model bilgisine sahip bir araç zaten mevcut.");

        mapper.Map(request, car);

        carRepository.Update(car);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}