using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.AboutFeatures.Commands.RemoveAbout;

public sealed class RemoveAboutCommandValidator : AbstractValidator<RemoveAboutCommand>
{
    public RemoveAboutCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}