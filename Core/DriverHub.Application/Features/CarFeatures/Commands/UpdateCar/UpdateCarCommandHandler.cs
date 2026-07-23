using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.CarFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Commands.UpdateCar;

public sealed class UpdateCarCommandHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCarCommand>
{
    public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            throw new NotFoundException();

        bool brandExists = await brandRepository.AnyAsync(b => b.Id == request.BrandId, cancellationToken);
        if (!brandExists)
            throw new NotFoundException("Marka bilgisi bulunamadı.");

        request.ApplyTo(car);

        carRepository.Update(car);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}