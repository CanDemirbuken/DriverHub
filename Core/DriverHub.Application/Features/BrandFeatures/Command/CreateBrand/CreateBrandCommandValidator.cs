using FluentValidation;

namespace DriverHub.Application.Features.BrandFeatures.Command.CreateBrand;

public sealed class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .WithMessage("Marka bilgisi boş bırakılamaz.")
            .MaximumLength(100)
            .WithMessage("Marka bilgisi en fazla 100 karakter olabilir");
    }
}