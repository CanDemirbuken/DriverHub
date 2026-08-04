using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.UserRole;
using DriverHub.Application.Interfaces.Identity;
using MediatR;

namespace DriverHub.Application.Features.Identity.UserRoleFeatures.Commands.RemoveRoleFromUser;

public sealed class RemoveRoleFromUserCommandHandler(IUserRoleService userRoleService) : IRequestHandler<RemoveRoleFromUserCommand, Result>
{
    public async Task<Result> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        var removeRoleRequest = new RemoveUserRoleRequest(request.UserId, request.RoleId);
        return await userRoleService.RemoveRoleAsync(removeRoleRequest, cancellationToken);
    }
}