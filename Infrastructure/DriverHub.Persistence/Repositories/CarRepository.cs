using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.Repositories;

public sealed class CarRepository(AppDbContext context) : Repository<Car>(context), ICarRepository
{
    public async Task<IReadOnlyList<Car>> GetAllCarWithBrandAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(car => car.Brand)
            .ToListAsync(cancellationToken);
    }

    public async Task<Car?> GetCarByIdWithBrandAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(car => car.Brand)
            .FirstOrDefaultAsync(
                car => car.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Car>> GetPagedCarsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .OrderByDescending(car => car.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .CountAsync(cancellationToken);
    }
}