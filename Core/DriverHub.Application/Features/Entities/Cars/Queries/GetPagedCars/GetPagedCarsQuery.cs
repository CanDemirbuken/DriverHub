using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Queries.GetPagedCars;

public sealed record GetPagedCarsQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<PagedResponse<GetPagedCarsQueryResponse>>>;