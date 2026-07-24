using FluentValidation;

namespace DriverHub.Application.Features.AuthFeatures.Commands.LoginUser;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage("E-mail bilgisi boş bırakılamaz.")
            .EmailAddress()
            .WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("Şifre bilgisi boş bırakılamaz.");
    }
}