using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetBrandById;

public sealed record GetBrandByIdQuery(Guid Id) : IRequest<GetBrandByIdQueryResponse>;