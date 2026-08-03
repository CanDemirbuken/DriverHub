using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.FeatureFeatures.Commands.UpdateFeature;

public sealed class UpdateFeatureCommandValidator : AbstractValidator<UpdateFeatureCommand>
{
    public UpdateFeatureCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Güncellenecek kaydın id bilgisi boş bırakılamaz.");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Özellik alanı zorunludur.")
            .MaximumLength(100)
            .WithMessage("Özellik en fazla 100 karakter olabilir.");
    }
}