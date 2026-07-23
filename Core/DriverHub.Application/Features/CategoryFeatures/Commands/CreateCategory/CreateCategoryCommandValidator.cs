using FluentValidation;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Kategori alanı boş bırakılamaz.")
            .MaximumLength(100)
            .WithMessage("Kategori alanı en fazla 100 karakter olabilir.");
    }
}