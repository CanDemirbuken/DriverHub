using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.Entities.CategoryFeatures.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(string Name) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}