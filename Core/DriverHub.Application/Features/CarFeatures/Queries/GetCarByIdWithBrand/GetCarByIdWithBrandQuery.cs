using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed record GetCarByIdWithBrandQuery(Guid Id) : IRequest<GetCarByIdWithBrandQueryResponse>;