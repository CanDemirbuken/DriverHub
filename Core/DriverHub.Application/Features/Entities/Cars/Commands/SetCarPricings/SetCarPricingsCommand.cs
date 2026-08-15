using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.SetCarPricings;

public sealed record SetCarPricingsCommand(
    Guid Id,
    IReadOnlyCollection<SetCarPricingItem> Pricings
) : IRequest<Result>;