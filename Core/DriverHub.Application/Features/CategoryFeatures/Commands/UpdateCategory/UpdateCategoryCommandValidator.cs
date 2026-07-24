using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Güncellenecek kaydın Id bilgisi boş bırakılamaz.");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Kategori bilgisi boş bırakılamaz.")
            .MaximumLength(100)
            .WithMessage("Kategori bilgisi en fazla 100 karakter olabilir.");
    }
}