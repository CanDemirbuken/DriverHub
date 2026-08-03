using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.ContactFeatures.Queries.GetContactById;

public sealed record GetContactByIdQuery(Guid Id) : IRequest<Result<GetContactByIdQueryResponse>>;