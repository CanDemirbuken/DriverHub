using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Commands.UpdateRole;

public sealed record UpdateRoleCommand : IRequest<Result>
{
    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}