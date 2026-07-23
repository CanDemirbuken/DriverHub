using DriverHub.Application.Common.Models;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCars;

public sealed record GetPagedCarsQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetPagedCarsQueryResponse>>;