using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.Entities.Cars.Queries.GetCarById;
using DriverHub.Application.Features.Entities.Cars.Queries.GetPagedCars;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class CarQueryService(AppDbContext context) : ICarQueryService
{
    public async Task<GetCarByIdQueryResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context
            .Set<Car>()
            .AsNoTracking()
            .Where(car => car.Id == id)
            .Select(car => new GetCarByIdQueryResponse(
                car.Id,
                car.BrandId,
                car.Brand!.Name,
                car.CategoryId,
                car.Category!.Name,
                car.CurrentLocationId,
                car.CurrentLocation!.Name,
                car.Model,
                car.ModelYear,
                car.Plate,
                car.Vin,
                car.CoverImageUrl,
                car.Km,
                car.Transmission,
                car.Seat,
                car.Luggage,
                car.Fuel,
                car.Color,
                car.Status,
                car.BigImageUrl,
                car.CarFeatures
                .Select(carFeature => new GetCarByIdQueryResponse.FeatureItem(
                    carFeature.FeatureId,
                    carFeature.Feature!.Name))
                .ToList(),
                car.CarPricings
                .Select(carPricing => new GetCarByIdQueryResponse.PricingItem(
                    carPricing.Type,
                    carPricing.Amount))
               .ToList()))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResponse<GetPagedCarsQueryResponse>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        IQueryable<Car> query = context
            .Set<Car>()
            .AsNoTracking();

        int totalCount = await query.CountAsync(cancellationToken);

        List<GetPagedCarsQueryResponse> items = await query
            .OrderByDescending(car => car.CreatedDate)
            .ThenByDescending(car => car.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(car => new GetPagedCarsQueryResponse(
                car.Id,
                car.CoverImageUrl,
                car.Plate,
                car.Brand!.Name,
                car.Model,
                car.ModelYear,
                car.Category!.Name,
                car.CurrentLocation!.Name,
                car.Km,
                car.Transmission,
                car.Fuel,
                car.Status))
            .ToListAsync(cancellationToken);

        return PagedResponse<GetPagedCarsQueryResponse>.CreateResponse(
            items,
            pageNumber,
            pageSize,
            totalCount);
    }
}