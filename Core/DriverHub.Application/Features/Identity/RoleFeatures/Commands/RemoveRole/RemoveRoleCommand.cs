using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.RemoveRole;

public sealed record RemoveRoleCommand(string Id) : IRequest<Result>;