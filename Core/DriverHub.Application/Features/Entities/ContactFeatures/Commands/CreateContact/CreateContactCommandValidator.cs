using FluentValidation;

namespace DriverHub.Application.Features.Entities.ContactFeatures.Commands.CreateContact;

public sealed class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ad Soyad alanı zorunludur.")
            .MaximumLength(150)
            .WithMessage("Ad Soyad en fazla 150 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-posta adresi zorunludur.")
            .EmailAddress()
            .WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(254)
            .WithMessage("E-posta adresi en fazla 254 karakter olabilir.");

        RuleFor(x => x.Subject)
            .NotEmpty()
            .WithMessage("Konu alanı zorunludur.")
            .MaximumLength(250)
            .WithMessage("Konu en fazla 250 karakter olabilir.");

        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Mesaj alanı zorunludur.")
            .MinimumLength(10)
            .WithMessage("Mesaj en az 10 karakter olmalıdır.")
            .MaximumLength(4000)
            .WithMessage("Mesaj en fazla 4000 karakter olabilir.");
    }
}