using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarById;

public sealed record GetCarByIdQuery(Guid Id) : IRequest<GetCarByIdQueryResponse>;