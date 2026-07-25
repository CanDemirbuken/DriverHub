using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Authentication.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<Result<LoginUserCommandResponse>>;