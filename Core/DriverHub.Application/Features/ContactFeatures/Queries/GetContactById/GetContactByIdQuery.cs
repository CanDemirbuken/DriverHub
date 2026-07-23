using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;

public sealed record GetContactByIdQuery(Guid Id) : IRequest<GetContactByIdQueryResponse>;