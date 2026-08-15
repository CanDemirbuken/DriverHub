using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.CreateCar;

public sealed class CreateCarCommandHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository, IRepository<Category> categoryRepository, IRepository<Location> locationRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCarCommand, Result<CreateCarCommandResponse>>
{
    public async Task<Result<CreateCarCommandResponse>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        bool brandExists = await brandRepository.AnyAsync(b => b.Id == request.BrandId, cancellationToken);
        if (!brandExists)
            return Result<CreateCarCommandResponse>.Failure(Error.NotFound("Marka bilgisi bulunamadı.", nameof(request.BrandId)));

        bool categoryExists = await categoryRepository.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
        if (!categoryExists)
            return Result<CreateCarCommandResponse>.Failure(Error.NotFound("Kategori bilgisi bulunamadı.", nameof(request.CategoryId)));

        bool locationExists = await locationRepository.AnyAsync(l => l.Id == request.CurrentLocationId, cancellationToken);
        if (!locationExists)
            return Result<CreateCarCommandResponse>.Failure(Error.NotFound("Lokasyon bilgisi bulunamadı.", nameof(request.CurrentLocationId)));

        bool plateExists = await carRepository.AnyAsync(c => c.Plate == request.Plate, cancellationToken);
        if (plateExists)
            return Result<CreateCarCommandResponse>.Failure(Error.Conflict("Bu plakaya sahip bir araç zaten mevcut.", nameof(request.Plate)));

        bool vinExists = await carRepository.AnyAsync(c => c.Vin == request.Vin, cancellationToken);
        if (vinExists)
            return Result<CreateCarCommandResponse>.Failure(Error.Conflict("Bu VIN numarasına sahip bir araç zaten mevcut.", nameof(request.Vin)));

        Car car = mapper.Map<Car>(request);
        car.Status = CarStatus.Active;

        await carRepository.AddAsync(car, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateCarCommandResponse data = new CreateCarCommandResponse(car.Id);
        return Result<CreateCarCommandResponse>.Success(data);
    }
}