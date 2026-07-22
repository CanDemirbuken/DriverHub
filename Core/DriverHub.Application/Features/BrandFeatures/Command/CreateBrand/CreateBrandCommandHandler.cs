using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Command.CreateBrand;

public sealed class CreateBrandCommandHandler(IRepository<Brand> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBrandCommand>
{
    public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand brand = new Brand()
        {
            Name = request.Name
        };

        await repository.AddAsync(brand, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}