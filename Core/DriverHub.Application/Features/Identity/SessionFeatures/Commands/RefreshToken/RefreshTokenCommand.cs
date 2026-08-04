using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Token.RefreshToken;
using MediatR;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<RefreshSessionResponse>>;