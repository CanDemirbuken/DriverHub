using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;

public sealed class GetPagedCarsWithBrandQueryHandler(ICarQueryService carQueryService) : IRequestHandler<GetPagedCarsWithBrandQuery, Result<PagedResponse<GetPagedCarsWithBrandQueryResponse>>>
{
    public async Task<Result<PagedResponse<GetPagedCarsWithBrandQueryResponse>>> Handle(GetPagedCarsWithBrandQuery request, CancellationToken cancellationToken)
    {
        var data =  await carQueryService.GetPagedWithBrandAsync(request.PageNumber, request.PageSize, cancellationToken);
        return Result<PagedResponse<GetPagedCarsWithBrandQueryResponse>>.Success(data, StatusCodes.Status200OK);
    }
}