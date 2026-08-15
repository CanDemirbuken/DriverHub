using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Locations.Commands.UpdateLocation;

public sealed class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Güncellenecek lokasyonun Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Lokasyon adı zorunludur.")
            .MaximumLength(150)
            .WithMessage("Lokasyon adı en fazla 150 karakter olabilir.");
    }
}