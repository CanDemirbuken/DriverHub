using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.CategoryFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(IRepository<Category> repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
            throw new NotFoundException();

        request.ApplyTo(category);

        repository.Update(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}