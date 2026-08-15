using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.SetCarPricings;

public sealed class SetCarPricingsCommandHandler(IRepository<Car> carRepository, IRepository<CarPricing> carPricingRepository, IUnitOfWork unitOfWork) : IRequestHandler<SetCarPricingsCommand, Result>
{
    public async Task<Result> Handle(SetCarPricingsCommand request, CancellationToken cancellationToken)
    {
        Car? car = await carRepository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip araç bulunamadı.", nameof(request.Id)));

        IReadOnlyList<CarPricing> existingPricings = await carPricingRepository.WhereAsync(
            carPricing => carPricing.CarId == request.Id,
            cancellationToken);

        foreach (SetCarPricingItem pricing in request.Pricings)
        {
            CarPricing? existingPricing = existingPricings
                .FirstOrDefault(x => x.Type == pricing.Type);

            if (existingPricing is null)
            {
                await carPricingRepository.AddAsync(
                    new CarPricing
                    {
                        CarId = request.Id,
                        Type = pricing.Type,
                        Amount = pricing.Amount
                    },
                    cancellationToken);

                continue;
            }

            existingPricing.Amount = pricing.Amount;
            carPricingRepository.Update(existingPricing);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}