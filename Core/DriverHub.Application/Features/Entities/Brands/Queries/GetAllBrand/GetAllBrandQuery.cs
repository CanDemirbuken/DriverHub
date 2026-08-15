using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Brands.Queries.GetAllBrand;

public sealed record GetAllBrandQuery : IRequest<Result<IReadOnlyList<GetAllBrandQueryResponse>>>;