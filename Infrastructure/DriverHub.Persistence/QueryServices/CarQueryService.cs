using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class CarQueryService(AppDbContext context) : ICarQueryService
{
    public async Task<PagedResponse<GetPagedCarsWithBrandQueryResponse>> GetPagedWithBrandAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        IQueryable<Car> query = context
            .Set<Car>()
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(car => car.CreatedDate)
            .ThenByDescending(car => car.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(car => new GetPagedCarsWithBrandQueryResponse(
                car.Id,
                car.Brand!.Name,
                car.Model,
                car.Km,
                car.Transmission,
                car.Fuel))
            .ToListAsync(cancellationToken);

        return PagedResponse<GetPagedCarsWithBrandQueryResponse>.CreateResponse(items, pageNumber, pageSize, totalCount);
    }
}