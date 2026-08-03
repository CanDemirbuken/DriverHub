using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.ContactFeatures.Commands.RemoveContact;

public sealed record RemoveContactCommand(Guid Id) : IRequest<Result>;