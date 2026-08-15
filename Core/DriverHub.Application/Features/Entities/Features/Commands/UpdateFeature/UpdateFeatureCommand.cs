using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.Entities.Features.Commands.UpdateFeature;

public sealed record UpdateFeatureCommand(
    string Name
) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}