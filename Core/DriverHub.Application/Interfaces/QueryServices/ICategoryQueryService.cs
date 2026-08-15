using DriverHub.Application.Features.Entities.Categories.Queries.GetAllCategory;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface ICategoryQueryService
{
    Task<IReadOnlyList<GetAllCategoryQueryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}