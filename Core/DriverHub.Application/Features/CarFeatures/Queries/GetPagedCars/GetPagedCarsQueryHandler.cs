using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.CarFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCars;

public sealed class GetPagedCarsQueryHandler(ICarRepository carRepository) : IRequestHandler<GetPagedCarsQuery, PagedResponse<GetPagedCarsQueryResponse>>
{
    public async Task<PagedResponse<GetPagedCarsQueryResponse>> Handle(GetPagedCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await carRepository.GetPagedCarsAsync(request.PageNumber, request.PageSize, cancellationToken);
        var totalCount = await carRepository.CountAsync(cancellationToken);

        IReadOnlyList<GetPagedCarsQueryResponse> items = cars
            .Select(c => c.ToGetPagedCarsResponse())
            .ToList();

        return PagedResponse<GetPagedCarsQueryResponse>.CreateResponse(items, request.PageNumber, request.PageSize, totalCount);
    }
}