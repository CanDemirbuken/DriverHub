using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.BrandFeatures.Commands.CreateBrand;

public sealed class CreateBrandCommandHandler(IRepository<Brand> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateBrandCommand, Result<CreateBrandCommandResponse>>
{
    public async Task<Result<CreateBrandCommandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        bool brandExists = await repository.AnyAsync(predicate: b => b.Name == request.Name, cancellationToken);
        if (brandExists)
            return Result<CreateBrandCommandResponse>.Failure(Error.Conflict("Bu isimde bir marka zaten mevcut.", nameof(request.Name)));

        Brand brand = mapper.Map<Brand>(request);

        await repository.AddAsync(brand, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateBrandCommandResponse data = new CreateBrandCommandResponse(brand.Id);
        return Result<CreateBrandCommandResponse>.Success(data);
    }
}