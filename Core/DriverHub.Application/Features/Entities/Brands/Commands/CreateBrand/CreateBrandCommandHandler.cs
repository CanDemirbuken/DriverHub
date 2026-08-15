using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Brands.Commands.CreateBrand;

public sealed class CreateBrandCommandHandler(IRepository<Brand> brandRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateBrandCommand, Result<CreateBrandCommandResponse>>
{
    public async Task<Result<CreateBrandCommandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        bool brandExists = await brandRepository.AnyAsync(
            brand => brand.Name == request.Name,
            cancellationToken);

        if (brandExists)
            return Result<CreateBrandCommandResponse>.Failure(
                Error.Conflict("Bu marka adına sahip bir kayıt zaten mevcut.", nameof(request.Name)));

        Brand brand = mapper.Map<Brand>(request);

        await brandRepository.AddAsync(brand, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateBrandCommandResponse data = new CreateBrandCommandResponse(brand.Id);

        return Result<CreateBrandCommandResponse>.Success(data);
    }
}