using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Güncellenecek kategorinin Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Kategori adı zorunludur.")
            .MaximumLength(100)
            .WithMessage("Kategori adı en fazla 100 karakter olabilir.");
    }
}