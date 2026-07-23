using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Commands.RemoveContact;

public sealed record RemoveContactCommand(Guid Id) : IRequest;