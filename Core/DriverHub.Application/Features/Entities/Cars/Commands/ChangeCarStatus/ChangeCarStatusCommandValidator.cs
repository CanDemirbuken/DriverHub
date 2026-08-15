using DriverHub.Application.Common.Validations;
using DriverHub.Domain.Enums;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Cars.Commands.ChangeCarStatus;

public sealed class ChangeCarStatusCommandValidator : AbstractValidator<ChangeCarStatusCommand>
{
    public ChangeCarStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Durumu güncellenecek aracın Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Geçerli bir araç durumu seçilmelidir.")
            .Must(status => status != 0)
            .WithMessage("Araç durumu boş bırakılamaz.");
    }
}