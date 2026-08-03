using DriverHub.Application.Features.Entities.BrandFeatures.Queries.GetAllBrand;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class BrandQueryService(AppDbContext context) : IBrandQueryService
{
    public async Task<IReadOnlyList<GetAllBrandQueryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Brand>()
            .AsNoTracking()
            .OrderBy(brand => brand.Name)
            .ThenBy(brand => brand.Id)
            .Select(brand => new GetAllBrandQueryResponse(
                brand.Id,
                brand.Name))
            .ToListAsync(cancellationToken);
    }
}