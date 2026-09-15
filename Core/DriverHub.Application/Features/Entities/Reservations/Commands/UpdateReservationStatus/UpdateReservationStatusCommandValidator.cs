using DriverHub.Domain.Enums;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Reservations.Commands.UpdateReservationStatus;

public sealed class UpdateReservationStatusCommandValidator : AbstractValidator<UpdateReservationStatusCommand>
{
    public UpdateReservationStatusCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.ActorId).NotEmpty().MaximumLength(450);
        RuleFor(x => x.Status).Must(status => status is ReservationStatus.Confirmed or ReservationStatus.Cancelled);
    }
}
