using DriverHub.Application.Features.Entities.Brands.Queries.GetAllBrand;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IBrandQueryService
{
    Task<IReadOnlyList<GetAllBrandQueryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}