using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.ConfirmEmail;

public sealed record ConfirmEmailCommand(string UserId, string ConfirmationToken) : IRequest<Result>;