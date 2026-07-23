using DriverHub.Application.Features.CategoryFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler(IRepository<Category> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = request.ToEntity();

        await repository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateCategoryCommandResponse response = new CreateCategoryCommandResponse(category.Id);
        return response;
    }
}