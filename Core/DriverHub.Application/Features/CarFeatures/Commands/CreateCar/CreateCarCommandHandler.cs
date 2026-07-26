using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.CarFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.CarFeatures.Commands.CreateCar;

public sealed class CreateCarCommandHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCarCommand, Result<CreateCarCommandResponse>>
{
    public async Task<Result<CreateCarCommandResponse>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        bool brandExists = await brandRepository.AnyAsync(b => b.Id == request.BrandId, cancellationToken);
        if (!brandExists)
            return Result<CreateCarCommandResponse>.Failure(StatusCodes.Status404NotFound, "Marka bilgisi bulunamadı.");

        bool carExists = await carRepository.AnyAsync(predicate: c => c.Model == request.Model && c.BrandId == request.BrandId, cancellationToken);
        if (carExists)
            return Result<CreateCarCommandResponse>.Failure(StatusCodes.Status409Conflict, "Bu marka ve model bilgisine sahip bir araç zaten mevcut.");

        Car car = request.ToEntity();

        await carRepository.AddAsync(car, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateCarCommandResponse data = new CreateCarCommandResponse(car.Id);
        return Result<CreateCarCommandResponse>.Success(data, StatusCodes.Status201Created);
    }
}