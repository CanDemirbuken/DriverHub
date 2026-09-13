using FluentValidation;
using DriverHub.Application.Features.Entities.Reservations.Common;

namespace DriverHub.Application.Features.Entities.Reservations.Commands.CreateReservation;

public sealed class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı bilgisi zorunludur.");
        RuleFor(x => x.CarId).NotEmpty().WithMessage("Araç seçimi zorunludur.");
        RuleFor(x => x.PickupLocationId).NotEmpty().WithMessage("Teslim alma lokasyonu zorunludur.");
        RuleFor(x => x.StartDate).Must(ReservationTimePolicy.IsStartAllowed).WithMessage("Başlangıç tarihi geçmişte olamaz.");
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");
        RuleFor(x => x.ExtraIds).Must(ids => ids.Distinct().Count() == ids.Count).WithMessage("Aynı ek hizmet birden fazla seçilemez.");
    }

}
