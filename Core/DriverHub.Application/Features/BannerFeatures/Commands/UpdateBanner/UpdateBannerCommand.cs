using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.BannerFeatures.Commands.UpdateBanner;

public sealed record UpdateBannerCommand(string Title, string Description, string VideoDescription, string VideoUrl) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}