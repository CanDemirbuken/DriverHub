using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.CarFeatures.Commands.UpdateCar;

public sealed record UpdateCarCommand(Guid BrandId, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, byte Luggage, string Fuel, string BigImageUrl) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}