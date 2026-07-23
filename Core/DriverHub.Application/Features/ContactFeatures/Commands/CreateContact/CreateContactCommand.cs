using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Commands.CreateContact;

public sealed record CreateContactCommand(string Name, string Email, string Subject, string Message) : IRequest<CreateContactCommandResponse>;