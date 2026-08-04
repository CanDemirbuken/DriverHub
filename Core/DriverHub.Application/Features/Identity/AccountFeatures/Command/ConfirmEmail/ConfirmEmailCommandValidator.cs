using DriverHub.Application.Features.Identity.AccountFeatures.Command.ConfirmEmail;
using FluentValidation;

public sealed class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.ConfirmationToken)
            .NotEmpty();
    }
}