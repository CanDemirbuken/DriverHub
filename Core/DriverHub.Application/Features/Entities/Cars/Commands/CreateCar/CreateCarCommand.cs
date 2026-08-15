using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.CreateCar;

public sealed record CreateCarCommand(Guid BrandId, Guid CategoryId, Guid CurrentLocationId, string Model, short ModelYear, string Plate, string Vin, int Km, string Transmission, byte Seat, int Luggage, string Fuel, string Color, string CoverImageUrl, string BigImageUrl) : IRequest<Result<CreateCarCommandResponse>>;