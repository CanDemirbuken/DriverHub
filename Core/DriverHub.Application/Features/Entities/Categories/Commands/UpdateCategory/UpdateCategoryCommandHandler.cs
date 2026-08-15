using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, Result>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category is null)
            return Result.Failure(
                Error.NotFound($"{request.Id} kimlik bilgisine sahip kategori bulunamadı.", nameof(request.Id)));

        bool categoryExists = await categoryRepository.AnyAsync(
            category => category.Id != request.Id && category.Name == request.Name,
            cancellationToken);

        if (categoryExists)
            return Result.Failure(
                Error.Conflict("Bu kategori adına sahip bir kayıt zaten mevcut.", nameof(request.Name)));

        mapper.Map(request, category);

        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}