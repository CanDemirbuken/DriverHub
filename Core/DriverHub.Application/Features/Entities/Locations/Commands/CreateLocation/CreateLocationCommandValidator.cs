using FluentValidation;

namespace DriverHub.Application.Features.Entities.Locations.Commands.CreateLocation;

public sealed class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Lokasyon adı zorunludur.")
            .MaximumLength(150)
            .WithMessage("Lokasyon adı en fazla 150 karakter olabilir.");
    }
}