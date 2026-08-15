using FluentValidation;

namespace DriverHub.Application.Features.Entities.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Kategori adı zorunludur.")
            .MaximumLength(100)
            .WithMessage("Kategori adı en fazla 100 karakter olabilir.");
    }
}