using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Queries.GetAllLocation;

public sealed record GetAllLocationQuery()
    : IRequest<Result<IReadOnlyList<GetAllLocationQueryResponse>>>;