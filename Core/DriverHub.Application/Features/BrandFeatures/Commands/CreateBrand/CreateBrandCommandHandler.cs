using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.BrandFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Commands.CreateBrand;

public sealed class CreateBrandCommandHandler(IRepository<Brand> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBrandCommand, CreateBrandCommandResponse>
{
    public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        bool brandExists = await repository.AnyAsync(predicate: b => b.Name == request.Name, cancellationToken);
        if (brandExists)
            throw new ConflictException("Bu isimde bir marka zaten mevcut.");

        Brand brand = request.ToEntity();

        await repository.AddAsync(brand, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateBrandCommandResponse response = new CreateBrandCommandResponse(brand.Id);
        return response;
    }
}