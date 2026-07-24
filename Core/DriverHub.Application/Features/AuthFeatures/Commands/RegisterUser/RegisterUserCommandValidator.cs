using FluentValidation;

namespace DriverHub.Application.Features.AuthFeatures.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty()
            .WithMessage("Ad bilgisi boş bırakılamaz.");

        RuleFor(command => command.LastName)
            .NotEmpty()
            .WithMessage("Soyad bilgisi boş bırakılamaz.");

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