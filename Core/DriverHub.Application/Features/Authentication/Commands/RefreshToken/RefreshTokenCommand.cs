using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;
using MediatR;

namespace DriverHub.Application.Features.Authentication.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<RefreshSessionResponse>>;