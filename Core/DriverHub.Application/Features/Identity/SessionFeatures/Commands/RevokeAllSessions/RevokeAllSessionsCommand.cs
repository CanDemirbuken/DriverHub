using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeAllSessions;

public sealed record RevokeAllSessionsCommand(string UserId) : IRequest<Result>;