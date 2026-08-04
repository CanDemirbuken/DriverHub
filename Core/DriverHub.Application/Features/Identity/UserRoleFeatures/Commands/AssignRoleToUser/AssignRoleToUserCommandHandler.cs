using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.UserRole;
using DriverHub.Application.Interfaces.Identity;
using MediatR;

namespace DriverHub.Application.Features.Identity.UserRoleFeatures.Commands.AssignRoleToUser;

public sealed class AssignRoleToUserCommandHandler(IUserRoleService userRoleService) : IRequestHandler<AssignRoleToUserCommand, Result>
{
    public async Task<Result> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var assignRoleRequest = new AssignRoleRequest(request.UserId, request.RoleId);
        return await userRoleService.AssignRoleAsync(assignRoleRequest, cancellationToken);
    }
}