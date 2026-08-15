using FluentValidation;

namespace DriverHub.Application.Features.Entities.Brands.Commands.CreateBrand;

public sealed class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Marka adı zorunludur.")
            .MaximumLength(100)
            .WithMessage("Marka adı en fazla 100 karakter olabilir.");
    }
}