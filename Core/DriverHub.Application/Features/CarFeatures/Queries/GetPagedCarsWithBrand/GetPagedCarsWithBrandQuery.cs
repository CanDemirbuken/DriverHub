using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;

public sealed record GetPagedCarsWithBrandQuery(int PageNumber, int PageSize) : IRequest<Result<PagedResponse<GetPagedCarsWithBrandQueryResponse>>>;