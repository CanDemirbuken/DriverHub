using DriverHub.Application.Common.Models;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;

public sealed record GetPagedCarsWithBrandQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetPagedCarsWithBrandQueryResponse>>;