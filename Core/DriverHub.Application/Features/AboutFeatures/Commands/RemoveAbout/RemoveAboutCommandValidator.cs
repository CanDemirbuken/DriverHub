using FluentValidation;

namespace DriverHub.Application.Features.AboutFeatures.Commands.RemoveAbout;

public sealed class RemoveAboutCommandValidator : AbstractValidator<RemoveAboutCommand>
{
    public RemoveAboutCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}