using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Queries.GetCarById;

public sealed record GetCarByIdQuery(Guid Id) : IRequest<Result<GetCarByIdQueryResponse>>;