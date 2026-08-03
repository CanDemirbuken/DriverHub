using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.AuthenticationFeatures.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<Result<LoginUserCommandResponse>>;