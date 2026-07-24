using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.RemoveCategory;

public sealed class RemoveCategoryCommandValidator : AbstractValidator<RemoveCategoryCommand>
{
    public RemoveCategoryCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}