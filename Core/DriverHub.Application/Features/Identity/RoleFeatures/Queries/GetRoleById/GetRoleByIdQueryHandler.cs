using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices.Identity;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetRoleById;

public sealed class GetRoleByIdQueryHandler(IRoleQueryService roleQueryService) : IRequestHandler<GetRoleByIdQuery, Result<GetRoleByIdQueryResponse>>
{
    public async Task<Result<GetRoleByIdQueryResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await roleQueryService.GetRoleByIdAsync(request.Id, cancellationToken);
        if (role == null)
            return Result<GetRoleByIdQueryResponse>.Failure(Error.NotFound($"{request.Id} kimliğine sahip kayıt bulunamadı.", nameof(request.Id)));

        return Result<GetRoleByIdQueryResponse>.Success(role);
    }
}