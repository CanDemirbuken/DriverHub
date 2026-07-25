using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Authentication.Commands.RegisterUser;
using MediatR;

public sealed record RegisterUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Result<RegisterUserCommandResponse>>;