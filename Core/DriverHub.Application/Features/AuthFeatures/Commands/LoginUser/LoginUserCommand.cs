using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.AuthFeatures.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<Result<LoginUserCommandResponse>>;