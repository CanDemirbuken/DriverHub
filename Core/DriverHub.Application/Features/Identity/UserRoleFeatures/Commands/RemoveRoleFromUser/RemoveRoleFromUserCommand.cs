using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.UserRoleFeatures.Commands.RemoveRoleFromUser;

public sealed record RemoveRoleFromUserCommand(string UserId, string RoleId) : IRequest<Result>;