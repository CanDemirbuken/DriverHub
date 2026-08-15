using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Brands.Commands.UpdateBrand;

public sealed class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Güncellenecek markanın Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Marka adı zorunludur.")
            .MaximumLength(100)
            .WithMessage("Marka adı en fazla 100 karakter olabilir.");
    }
}