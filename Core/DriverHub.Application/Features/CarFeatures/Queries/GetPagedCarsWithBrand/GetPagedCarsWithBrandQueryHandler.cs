using DriverHub.Application.Common.Models;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;

public sealed class GetPagedCarsWithBrandQueryHandler(ICarQueryService carQueryService) : IRequestHandler<GetPagedCarsWithBrandQuery, PagedResponse<GetPagedCarsWithBrandQueryResponse>>
{
    public async Task<PagedResponse<GetPagedCarsWithBrandQueryResponse>> Handle(GetPagedCarsWithBrandQuery request, CancellationToken cancellationToken)
    {
        return await carQueryService.GetPagedWithBrandAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}