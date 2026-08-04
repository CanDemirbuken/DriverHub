using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Session;
using DriverHub.Application.Contracts.Identity.Token.RefreshToken;

namespace DriverHub.Application.Interfaces.Authentication;

public interface ISessionService
{
    Task<Result<SessionResponse>> CreateSessionAsync(CreateSessionRequest request, CancellationToken cancellationToken = default);
    Task<Result<RefreshSessionResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<Result> RevokeAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<Result> RevokeAllAsync(string userId, CancellationToken cancellationToken = default);
}