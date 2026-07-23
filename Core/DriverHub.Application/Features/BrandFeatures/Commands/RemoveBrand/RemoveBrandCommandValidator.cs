using FluentValidation;

namespace DriverHub.Application.Features.BrandFeatures.Commands.RemoveBrand;

public sealed class RemoveBrandCommandValidator : AbstractValidator<RemoveBrandCommand>
{
    public RemoveBrandCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}