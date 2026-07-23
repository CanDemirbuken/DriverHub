using FluentValidation;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.RemoveCategory;

public sealed class RemoveCategoryCommandValidator : AbstractValidator<RemoveCategoryCommand>
{
    public RemoveCategoryCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}