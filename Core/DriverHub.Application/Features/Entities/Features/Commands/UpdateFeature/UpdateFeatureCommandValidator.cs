using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Features.Commands.UpdateFeature;

public sealed class UpdateFeatureCommandValidator : AbstractValidator<UpdateFeatureCommand>
{
    public UpdateFeatureCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Güncellenecek özelliğin Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Özellik adı zorunludur.")
            .MaximumLength(100)
            .WithMessage("Özellik adı en fazla 100 karakter olabilir.");
    }
}