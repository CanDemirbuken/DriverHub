using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Commands.CreateCar;

public sealed record CreateCarCommand(Guid BrandId, string Model, string CoverImageUrl, int Km, string Transmission, byte Seat, byte Luggage, string Fuel, string BigImageUrl) : IRequest<Result<CreateCarCommandResponse>>;