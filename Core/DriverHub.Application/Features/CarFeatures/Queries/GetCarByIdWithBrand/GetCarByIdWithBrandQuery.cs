using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed record GetCarByIdWithBrandQuery(Guid Id) : IRequest<Result<GetCarByIdWithBrandQueryResponse>>;