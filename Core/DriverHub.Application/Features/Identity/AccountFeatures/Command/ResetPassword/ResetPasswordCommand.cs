using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.ResetPassword;

public sealed record ResetPasswordCommand(string Email, string ResetToken, string NewPassword) : IRequest<Result>;