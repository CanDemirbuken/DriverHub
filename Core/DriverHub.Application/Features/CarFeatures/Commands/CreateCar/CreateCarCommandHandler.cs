using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Commands.CreateCar;

public sealed class CreateCarCommandHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCarCommand, Result<CreateCarCommandResponse>>
{
    public async Task<Result<CreateCarCommandResponse>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        bool brandExists = await brandRepository.AnyAsync(b => b.Id == request.BrandId, cancellationToken);
        if (!brandExists)
            return Result<CreateCarCommandResponse>.Failure(Error.NotFound("Marka bilgisi bulunamadı.", nameof(request.BrandId)));

        bool carExists = await carRepository.AnyAsync(predicate: c => c.Model == request.Model && c.BrandId == request.BrandId, cancellationToken);
        if (carExists)
            return Result<CreateCarCommandResponse>.Failure(Error.Conflict("Bu marka ve model bilgisine sahip bir araç zaten mevcut.", nameof(request.Model)));

        Car car = mapper.Map<Car>(request);

        await carRepository.AddAsync(car, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateCarCommandResponse data = new CreateCarCommandResponse(car.Id);
        return Result<CreateCarCommandResponse>.Success(data);
    }
}