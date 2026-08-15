using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Brands.Commands.RemoveBrand;

public sealed class RemoveBrandCommandHandler(IRepository<Brand> brandRepository, IRepository<Car> carRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveBrandCommand, Result>
{
    public async Task<Result> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
            return Result.Failure(
                Error.NotFound($"{request.Id} kimlik bilgisine sahip marka bulunamadı.", nameof(request.Id)));

        bool hasCars = await carRepository.AnyAsync(
            car => car.BrandId == request.Id,
            cancellationToken);

        if (hasCars)
            return Result.Failure(
                Error.Conflict("Bu markaya bağlı araçlar bulunduğu için marka silinemez.", nameof(request.Id)));

        brandRepository.Remove(brand);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}