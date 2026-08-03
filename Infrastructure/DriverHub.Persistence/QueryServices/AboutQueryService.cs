using DriverHub.Application.Features.Entities.AboutFeatures.Queries.GetAllAbout;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class AboutQueryService(AppDbContext context) : IAboutQueryService
{
    public async Task<IReadOnlyList<GetAllAboutQueryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<About>()
            .AsNoTracking()
            .OrderByDescending(about => about.CreatedDate)
            .ThenByDescending(about => about.Id)
            .Select(about => new GetAllAboutQueryResponse(
                about.Id,
                about.Title,
                about.Description.Length > 50
                    ? about.Description.Substring(0, 50) + "..."
                    : about.Description,
                about.ImageUrl))
            .ToListAsync(cancellationToken);
    }
}