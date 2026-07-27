using FluentValidation;

namespace DriverHub.Application.Features.FeatureFeatures.Commands.CreateFeature;

public sealed class CreateFeatureCommandValidator : AbstractValidator<CreateFeatureCommand>
{
    public CreateFeatureCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Özellik alanı zorunludur.")
            .MaximumLength(100)
            .WithMessage("Özellik en fazla 100 karakter olabilir.");
    }
}