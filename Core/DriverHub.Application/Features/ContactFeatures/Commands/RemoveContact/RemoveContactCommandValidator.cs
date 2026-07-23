using FluentValidation;

namespace DriverHub.Application.Features.ContactFeatures.Commands.RemoveContact;

public sealed class RemoveContactCommandValidator : AbstractValidator<RemoveContactCommand>
{
    public RemoveContactCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}