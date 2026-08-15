using DriverHub.Application.Features.Entities.Categories.Queries.GetAllCategory;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class CategoryQueryService(AppDbContext context) : ICategoryQueryService
{
    public async Task<IReadOnlyList<GetAllCategoryQueryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Set<Category>()
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ThenBy(category => category.Id)
            .Select(category => new GetAllCategoryQueryResponse(
                category.Id,
                category.Name))
            .ToListAsync(cancellationToken);
    }
}