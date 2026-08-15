using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Brands.Commands.UpdateBrand;

public sealed class UpdateBrandCommandHandler(IRepository<Brand> brandRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateBrandCommand, Result>
{
    public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
            return Result.Failure(
                Error.NotFound($"{request.Id} kimlik bilgisine sahip marka bulunamadı.", nameof(request.Id)));

        bool brandExists = await brandRepository.AnyAsync(
            brand => brand.Id != request.Id && brand.Name == request.Name,
            cancellationToken);

        if (brandExists)
            return Result.Failure(
                Error.Conflict("Bu marka adına sahip bir kayıt zaten mevcut.", nameof(request.Name)));

        mapper.Map(request, brand);

        brandRepository.Update(brand);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}