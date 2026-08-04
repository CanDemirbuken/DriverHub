using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Role;
using DriverHub.Application.Interfaces.Identity;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.UpdateRole;

public sealed class UpdateRoleCommandHandler(IRoleService roleService) : IRequestHandler<UpdateRoleCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var updateRoleRequest = new UpdateRoleRequest(request.Id, request.Name);
        return await roleService.UpdateAsync(updateRoleRequest, cancellationToken);
    }
}