using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetAllCarWithBrand;

public sealed record GetAllCarWithBrandQuery : IRequest<IReadOnlyList<GetAllCarWithBrandQueryResponse>>;