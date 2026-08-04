using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.CreateRole;

public sealed record CreateRoleCommand(string Name) : IRequest<Result<CreateRoleCommandResponse>>;