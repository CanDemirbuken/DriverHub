using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetAllRoles;

public sealed record GetAllRolesQuery : IRequest<Result<IReadOnlyList<GetAllRolesQueryResponse>>>;