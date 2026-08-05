using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : IRequest<Result>;