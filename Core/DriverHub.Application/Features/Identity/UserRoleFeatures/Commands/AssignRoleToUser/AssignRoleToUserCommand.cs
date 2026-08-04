using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.UserRoleFeatures.Commands.AssignRoleToUser;

public sealed record AssignRoleToUserCommand(string UserId, string RoleId) : IRequest<Result>;