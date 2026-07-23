using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetAllCar;

public sealed record GetAllCarQuery : IRequest<IReadOnlyList<GetAllCarQueryResponse>>;