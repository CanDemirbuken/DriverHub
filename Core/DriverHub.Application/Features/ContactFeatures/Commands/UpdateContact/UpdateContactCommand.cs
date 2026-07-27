using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.ContactFeatures.Commands.UpdateContact;

public sealed record UpdateContactCommand(string Name, string Email, string Subject, string Message) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}