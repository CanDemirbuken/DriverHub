using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Commands.UpdateCar;

public sealed class UpdateCarCommandHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCarCommand, Result>
{
    public async Task<Result> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        bool brandExists = await brandRepository.AnyAsync(b => b.Id == request.BrandId, cancellationToken);
        if (!brandExists)
            return Result.Failure(Error.NotFound("Marka bilgisi bulunamadı.", nameof(request.BrandId)));

        bool carExists = await carRepository.AnyAsync(predicate: c => c.Id != request.Id && c.Model == request.Model && c.BrandId == request.BrandId, cancellationToken);
        if (carExists)
            return Result.Failure(Error.Conflict("Bu marka ve model bilgisine sahip bir araç zaten mevcut.", nameof(request.Model)));

        mapper.Map(request, car);

        carRepository.Update(car);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}