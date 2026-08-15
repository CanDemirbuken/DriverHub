using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Categories.Commands.RemoveCategory;

public sealed class RemoveCategoryCommandHandler(IRepository<Category> categoryRepository, IRepository<Car> carRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveCategoryCommand, Result>
{
    public async Task<Result> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category is null)
            return Result.Failure(
                Error.NotFound($"{request.Id} kimlik bilgisine sahip kategori bulunamadı.", nameof(request.Id)));

        bool hasCars = await carRepository.AnyAsync(
            car => car.CategoryId == request.Id,
            cancellationToken);

        if (hasCars)
            return Result.Failure(
                Error.Conflict("Bu kategoriye bağlı araçlar bulunduğu için kategori silinemez.", nameof(request.Id)));

        categoryRepository.Remove(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}