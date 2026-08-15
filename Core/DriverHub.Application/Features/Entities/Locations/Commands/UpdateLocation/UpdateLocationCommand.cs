using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.Entities.Locations.Commands.UpdateLocation;

public sealed record UpdateLocationCommand(
    string Name
) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}