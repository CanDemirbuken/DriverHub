using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices.Identity;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetAllRoles;

public sealed class GetAllRolesQueryHandler(IRoleQueryService roleQueryService) : IRequestHandler<GetAllRolesQuery, Result<IReadOnlyList<GetAllRolesQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllRolesQueryResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleQueryService.GetRolesAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllRolesQueryResponse>>.Success(roles);
    } 
}