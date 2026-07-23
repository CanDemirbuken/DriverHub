using FluentValidation;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Güncellenecek kaydın Id bilgisi boş bırakılamaz.");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Kategori alanı boş bırakılamaz.")
            .MaximumLength(100)
            .WithMessage("Kategori alanı en fazla 100 karakter olabilir.");
    }
}