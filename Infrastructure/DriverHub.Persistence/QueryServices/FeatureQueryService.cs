using DriverHub.Application.Features.FeatureFeatures.Queries.GetAllFeature;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class FeatureQueryService(AppDbContext context) : IFeatureQueryService
{
    public async Task<IReadOnlyList<GetAllFeatureQueryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Feature>()
            .AsNoTracking()
            .OrderBy(f => f.Name)
            .ThenBy(f => f.Id)
            .Select(f => new GetAllFeatureQueryResponse
            (
                f.Id,
                f.Name
            )).ToListAsync(cancellationToken);
    }
}