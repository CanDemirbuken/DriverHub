using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeSession;

public sealed record RevokeSessionCommand(string RefreshToken) : IRequest<Result>;