using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed record GetCarByIdWithBrandQuery(Guid Id) : IRequest<Result<GetCarByIdWithBrandQueryResponse>>;