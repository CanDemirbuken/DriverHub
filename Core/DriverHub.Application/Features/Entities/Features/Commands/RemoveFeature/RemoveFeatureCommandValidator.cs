using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Features.Commands.RemoveFeature;

public sealed class RemoveFeatureCommandValidator : AbstractValidator<RemoveFeatureCommand>
{
    public RemoveFeatureCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Silinecek özelliğin Id bilgisi boş bırakılamaz.");
    }
}