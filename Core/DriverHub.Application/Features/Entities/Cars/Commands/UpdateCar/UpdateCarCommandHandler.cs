using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.UpdateCar;

public sealed class UpdateCarCommandHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository, IRepository<Category> categoryRepository, IRepository<Location> locationRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCarCommand, Result>
{
    public async Task<Result> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        bool brandExists = await brandRepository.AnyAsync(b => b.Id == request.BrandId, cancellationToken);
        if (!brandExists)
            return Result.Failure(Error.NotFound("Marka bilgisi bulunamadı.", nameof(request.BrandId)));

        bool categoryExists = await categoryRepository.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
        if (!categoryExists)
            return Result.Failure(Error.NotFound("Kategori bilgisi bulunamadı.", nameof(request.CategoryId)));

        bool locationExists = await locationRepository.AnyAsync(l => l.Id == request.CurrentLocationId, cancellationToken);
        if (!locationExists)
            return Result.Failure(Error.NotFound("Lokasyon bilgisi bulunamadı.", nameof(request.CurrentLocationId)));

        bool plateExists = await carRepository.AnyAsync(c => c.Id != request.Id && c.Plate == request.Plate, cancellationToken);
        if (plateExists)
            return Result.Failure(Error.Conflict("Bu plakaya sahip başka bir araç zaten mevcut.", nameof(request.Plate)));

        bool vinExists = await carRepository.AnyAsync(c => c.Id != request.Id && c.Vin == request.Vin, cancellationToken);
        if (vinExists)
            return Result.Failure(Error.Conflict("Bu VIN numarasına sahip başka bir araç zaten mevcut.", nameof(request.Vin)));

        mapper.Map(request, car);

        carRepository.Update(car);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}