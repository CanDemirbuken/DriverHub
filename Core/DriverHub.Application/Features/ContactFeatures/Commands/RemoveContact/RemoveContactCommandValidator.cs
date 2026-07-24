using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.ContactFeatures.Commands.RemoveContact;

public sealed class RemoveContactCommandValidator : AbstractValidator<RemoveContactCommand>
{
    public RemoveContactCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}