using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Cars.Commands.ChangeCarLocation;

public sealed class ChangeCarLocationCommandValidator : AbstractValidator<ChangeCarLocationCommand>
{
    public ChangeCarLocationCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Lokasyonu güncellenecek aracın Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.CurrentLocationId)
            .ValidId("Yeni lokasyon bilgisi boş bırakılamaz.");
    }
}