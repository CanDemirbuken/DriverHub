using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.Entities.CarFeatures.Queries.GetPagedCarsWithBrand;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface ICarQueryService
{
    Task<PagedResponse<GetPagedCarsWithBrandQueryResponse>>
        GetPagedWithBrandAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);
}