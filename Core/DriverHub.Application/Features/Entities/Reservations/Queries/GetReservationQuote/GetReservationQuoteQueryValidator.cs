using FluentValidation;
using DriverHub.Application.Features.Entities.Reservations.Common;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetReservationQuote;

public sealed class GetReservationQuoteQueryValidator : AbstractValidator<GetReservationQuoteQuery>
{
    public GetReservationQuoteQueryValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.PickupLocationId).NotEmpty();
        RuleFor(x => x.StartDate).Must(ReservationTimePolicy.IsStartAllowed).WithMessage("Başlangıç tarihi geçmişte olamaz.");
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");
    }

}
