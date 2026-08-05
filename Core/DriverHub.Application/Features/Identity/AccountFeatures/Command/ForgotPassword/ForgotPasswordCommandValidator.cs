using FluentValidation;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.ForgotPassword;

public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage("Email bilgisi boş bırakılamaz.")
            .EmailAddress()
            .WithMessage("Lütfen geçerli bir email adresi girin.");
    }
}