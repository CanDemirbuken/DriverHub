using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.CarFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Commands.CreateCar;

public sealed class CreateCarCommandHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCarCommand, CreateCarCommandResponse>
{
    public async Task<CreateCarCommandResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        bool brandExists = await brandRepository.AnyAsync(b => b.Id == request.BrandId, cancellationToken);
        if (!brandExists)
            throw new NotFoundException("Marka bilgisi bulunamadı.");

        Car car = request.ToEntity();

        await carRepository.AddAsync(car, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateCarCommandResponse response = new CreateCarCommandResponse(car.Id);
        return response;
    }
}