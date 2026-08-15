using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Categories.Commands.RemoveCategory;

public sealed class RemoveCategoryCommandValidator : AbstractValidator<RemoveCategoryCommand>
{
    public RemoveCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Silinecek kategorinin Id bilgisi boş bırakılamaz.");
    }
}