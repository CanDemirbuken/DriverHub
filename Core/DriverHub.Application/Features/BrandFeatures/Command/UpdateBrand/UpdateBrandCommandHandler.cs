using DriverHub.Application.Exceptions;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Command.UpdateBrand
{
    public sealed class UpdateBrandCommandHandler(IRepository<Brand> repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateBrandCommand>
    {
        public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (brand is null)
                throw new NotFoundException();

            brand.Name = request.Name;

            repository.Update(brand);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
