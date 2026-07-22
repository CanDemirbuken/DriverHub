using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetAllBrand;

public sealed record GetAllBrandQuery : IRequest<IReadOnlyList<GetAllBrandQueryResponse>>;