using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Queries.GetPagedCars;

public sealed class GetPagedCarsQueryHandler(ICarQueryService carQueryService) : IRequestHandler<GetPagedCarsQuery, Result<PagedResponse<GetPagedCarsQueryResponse>>>
{
    public async Task<Result<PagedResponse<GetPagedCarsQueryResponse>>> Handle(GetPagedCarsQuery request, CancellationToken cancellationToken)
    {
        PagedResponse<GetPagedCarsQueryResponse> cars = await carQueryService.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return Result<PagedResponse<GetPagedCarsQueryResponse>>.Success(cars);
    }
}