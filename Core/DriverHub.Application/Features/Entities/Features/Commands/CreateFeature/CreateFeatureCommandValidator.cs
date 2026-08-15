using FluentValidation;

namespace DriverHub.Application.Features.Entities.Features.Commands.CreateFeature;

public sealed class CreateFeatureCommandValidator : AbstractValidator<CreateFeatureCommand>
{
    public CreateFeatureCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Özellik adı zorunludur.")
            .MaximumLength(100)
            .WithMessage("Özellik adı en fazla 100 karakter olabilir.");
    }
}