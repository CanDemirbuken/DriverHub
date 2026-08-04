using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Role;
using DriverHub.Application.Interfaces.Identity;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.CreateRole;

public sealed class CreateRoleCommandHandler(IRoleService roleService) : IRequestHandler<CreateRoleCommand, Result<CreateRoleCommandResponse>>
{
    public async Task<Result<CreateRoleCommandResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var createRoleRequest = new CreateRoleRequest(request.Name);

        var createRoleResult = await roleService.CreateAsync(createRoleRequest, cancellationToken);
        if (createRoleResult.IsFailure)
            return Result<CreateRoleCommandResponse>.Failure(createRoleResult.Errors);

        var createRoleCommandResponse = new CreateRoleCommandResponse(createRoleResult.Value.Id);
        return Result<CreateRoleCommandResponse>.Success(createRoleCommandResponse);
    }
}