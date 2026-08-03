using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.ContactFeatures.Commands.CreateContact;

public sealed record CreateContactCommand(string Name, string Email, string Subject, string Message) : IRequest<Result<CreateContactCommandResponse>>;