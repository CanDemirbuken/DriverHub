using FluentValidation;

namespace DriverHub.Application.Features.CarFeatures.Commands.RemoveCar;

public sealed class RemoveCarCommandValidator : AbstractValidator<RemoveCarCommand>
{
    public RemoveCarCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}