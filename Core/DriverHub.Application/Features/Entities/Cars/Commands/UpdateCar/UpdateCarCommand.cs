using DriverHub.Application.Common.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DriverHub.Application.Features.Entities.Cars.Commands.UpdateCar;

public sealed record UpdateCarCommand(Guid BrandId, Guid CategoryId, Guid CurrentLocationId, string Model, short ModelYear, string Plate, string Vin, string CoverImageUrl, int Km, string Transmission, byte Seat, int Luggage, string Fuel, string Color, string BigImageUrl) : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}