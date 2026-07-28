using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler(IRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryCommandResponse>>
{
    public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        bool categoryExists = await repository.AnyAsync(predicate: c => c.Name == request.Name, cancellationToken);
        if (categoryExists)
            return Result<CreateCategoryCommandResponse>.Failure(Error.Conflict("Bu isimde bir kategori zaten mevcut.", nameof(request.Name)));

        Category category = mapper.Map<Category>(request);

        await repository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        CreateCategoryCommandResponse data = new CreateCategoryCommandResponse(category.Id);
        return Result<CreateCategoryCommandResponse>.Success(data);
    }
}