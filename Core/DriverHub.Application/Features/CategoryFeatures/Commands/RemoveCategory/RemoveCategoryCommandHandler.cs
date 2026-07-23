using DriverHub.Application.Exceptions;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.RemoveCategory;

public sealed class RemoveCategoryCommandHandler(IRepository<Category> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveCategoryCommand>
{
    public async Task Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
            throw new NotFoundException();

        repository.Remove(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}