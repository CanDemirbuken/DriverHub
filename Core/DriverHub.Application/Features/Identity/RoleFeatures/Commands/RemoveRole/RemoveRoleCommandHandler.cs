using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Role;
using DriverHub.Application.Interfaces.Identity;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.RemoveRole;

public sealed class RemoveRoleCommandHandler(IRoleService roleService) : IRequestHandler<RemoveRoleCommand, Result>
{
    public async Task<Result> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        var removeRoleRequest = new RemoveRoleRequest(request.Id);
        return await roleService.RemoveAsync(removeRoleRequest, cancellationToken);
    }
}