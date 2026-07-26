using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetAllBrand;

public sealed record GetAllBrandQuery : IRequest<Result<IReadOnlyList<GetAllBrandQueryResponse>>>;