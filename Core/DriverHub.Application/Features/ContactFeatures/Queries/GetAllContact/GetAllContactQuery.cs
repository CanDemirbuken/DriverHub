using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetAllContact;

public sealed record GetAllContactQuery : IRequest<IReadOnlyList<GetAllContactQueryResponse>>;