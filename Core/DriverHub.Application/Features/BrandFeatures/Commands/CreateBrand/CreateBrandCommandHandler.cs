using DriverHub.Application.Features.BrandFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Commands.CreateBrand;

public sealed class CreateBrandCommandHandler(IRepository<Brand> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBrandCommand, CreateBrandCommandResponse>
{
    public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand brand = request.ToEntity();

        await repository.AddAsync(brand, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateBrandCommandResponse response = new CreateBrandCommandResponse(brand.Id);
        return response;
    }
}