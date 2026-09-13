using FluentValidation;
using DriverHub.Application.Features.Entities.Reservations.Common;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;

public sealed class GetAvailableCarsQueryValidator : AbstractValidator<GetAvailableCarsQuery>
{
    public GetAvailableCarsQueryValidator()
    {
        RuleFor(x => x.PickupLocationId)
            .NotEmpty()
            .WithMessage("Teslim alma lokasyonu zorunludur.");

        RuleFor(x => x.StartDate)
            .Must(ReservationTimePolicy.IsStartAllowed)
            .WithMessage("Başlangıç tarihi geçmişte olamaz.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");
    }

}
