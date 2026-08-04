using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetRoleById;

public sealed record GetRoleByIdQuery(string Id) : IRequest<Result<GetRoleByIdQueryResponse>>;