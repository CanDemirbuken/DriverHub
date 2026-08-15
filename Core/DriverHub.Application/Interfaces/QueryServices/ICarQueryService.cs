using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.Entities.Cars.Queries.GetCarById;
using DriverHub.Application.Features.Entities.Cars.Queries.GetPagedCars;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface ICarQueryService
{
    Task<GetCarByIdQueryResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<GetPagedCarsQueryResponse>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}