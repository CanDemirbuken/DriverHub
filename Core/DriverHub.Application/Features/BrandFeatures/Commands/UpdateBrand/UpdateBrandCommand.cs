using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.BrandFeatures.Commands.UpdateBrand;

public sealed record UpdateBrandCommand(string Name) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}