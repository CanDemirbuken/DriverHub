using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.Entities.AboutFeatures.Commands.UpdateAbout;

public sealed record UpdateAboutCommand(string Title, string Description, string ImageUrl) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}