using DriverHub.Domain.Entities;

namespace DriverHub.Application.Interfaces;

public interface ICarRepository : IRepository<Car>
{
    Task<IReadOnlyList<Car>> GetAllCarWithBrandAsync(
        CancellationToken cancellationToken = default);

    Task<Car?> GetCarByIdWithBrandAsync(
        Guid Id,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Car>> GetPagedCarsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}