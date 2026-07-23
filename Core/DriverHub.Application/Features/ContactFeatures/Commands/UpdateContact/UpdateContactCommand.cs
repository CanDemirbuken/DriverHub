using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Commands.UpdateContact;

public sealed record UpdateContactCommand(Guid Id, string Name, string Email, string Subject, string Message) : IRequest;