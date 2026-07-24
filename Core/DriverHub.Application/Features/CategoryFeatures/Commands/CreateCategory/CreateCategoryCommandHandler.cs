using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.CategoryFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler(IRepository<Category> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        bool categoryExists = await repository.AnyAsync(predicate: c => c.Name == request.Name, cancellationToken);
        if (categoryExists)
            throw new ConflictException("Bu isimde bir kategori zaten mevcut.");

        Category category = request.ToEntity();

        await repository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateCategoryCommandResponse response = new CreateCategoryCommandResponse(category.Id);
        return response;
    }
}