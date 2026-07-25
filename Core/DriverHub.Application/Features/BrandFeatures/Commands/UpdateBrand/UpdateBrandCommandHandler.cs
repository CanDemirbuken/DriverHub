using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.BrandFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Commands.UpdateBrand
{
    public sealed class UpdateBrandCommandHandler(IRepository<Brand> repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateBrandCommand>
    {
        public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (brand is null)
                throw new NotFoundException();

            request.ApplyTo(brand);

            repository.Update(brand);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
