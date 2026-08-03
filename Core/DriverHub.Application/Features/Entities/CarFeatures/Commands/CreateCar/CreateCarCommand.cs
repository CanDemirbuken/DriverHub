using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.CarFeatures.Commands.CreateCar;

public sealed record CreateCarCommand(Guid BrandId, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, int Luggage, string Fuel, string BigImageUrl) : IRequest<Result<CreateCarCommandResponse>>;