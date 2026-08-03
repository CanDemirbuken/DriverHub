using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.FeatureFeatures.Commands.RemoveFeature;

public sealed class RemoveFeatureCommandValidator : AbstractValidator<RemoveFeatureCommand>
{
    public RemoveFeatureCommandValidator()
    {
        RuleFor(command => command.Id)
            .ValidId("Silinecek kaydın Id bilgisi boş bırakılamaz.");
    }
}