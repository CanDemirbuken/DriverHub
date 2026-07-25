using FluentValidation;

namespace DriverHub.Application.Features.Authentication.Commands.RegisterUser;

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
            .WithMessage("Şifre bilgisi boş bırakılamaz.")
            .MinimumLength(8)
            .WithMessage("Şifre en az 8 karakter olmalıdır.")
            .Matches("[A-Z]")
            .WithMessage("Şifre en az bir büyük harf içermelidir.")
            .Matches("[a-z]")
            .WithMessage("Şifre en az bir küçük harf içermelidir.")
            .Matches("[0-9]")
            .WithMessage("Şifre en az bir rakam içermelidir.")
            .Matches(@"[\W_]")
            .WithMessage("Şifre en az bir özel karakter içermelidir.");
    }
}