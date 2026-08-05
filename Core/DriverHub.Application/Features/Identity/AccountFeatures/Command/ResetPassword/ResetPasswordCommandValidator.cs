using FluentValidation;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.ResetPassword;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage("Email bilgisi boş bırakılamaz.")
            .EmailAddress()
            .WithMessage("Lütfen geçerli bir email adresi girin.");

        RuleFor(command => command.ResetToken)
            .NotEmpty()
            .WithMessage("Şifre sıfırlama token bilgisi boş bırakılamaz.");

        RuleFor(command => command.NewPassword)
            .NotEmpty()
            .WithMessage("Yeni şifre bilgisi boş bırakılamaz.")
            .MinimumLength(8)
            .WithMessage("Yeni şifre en az 8 karakter olmalıdır.")
            .Matches("[A-Z]")
            .WithMessage("Yeni şifre en az bir büyük harf içermelidir.")
            .Matches("[a-z]")
            .WithMessage("Yeni şifre en az bir küçük harf içermelidir.")
            .Matches("[0-9]")
            .WithMessage("Yeni şifre en az bir rakam içermelidir.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Yeni şifre en az bir özel karakter içermelidir.");
    }
}