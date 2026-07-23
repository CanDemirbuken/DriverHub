using FluentValidation;

namespace DriverHub.Application.Features.BrandFeatures.Commands.UpdateBrand;

public sealed class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Güncellenecek kaydın Id bilgisi boş bırakılamaz.");

        RuleFor(b => b.Name)
            .NotEmpty()
            .WithMessage("Marka bilgisi boş bırakılamaz.")
            .MaximumLength(100)
            .WithMessage("Marka bilgisi en fazla 100 karakter olabilir.");
    }
}