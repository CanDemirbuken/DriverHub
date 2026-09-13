namespace DriverHub.Application.Features.Entities.Reservations.Commands.CreateReservation;

public sealed record CreateReservationCommandResponse(Guid Id, decimal BasePrice, decimal ExtraPrice, decimal InsurancePrice, decimal TotalPrice);
