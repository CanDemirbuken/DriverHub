using DriverHub.Application.Exceptions;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Commands.RemoveBrand;

public sealed class RemoveBrandCommandHandler(IRepository<Brand> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveBrandCommand>
{
    public async Task Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
            throw new NotFoundException();

        repository.Remove(brand);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}