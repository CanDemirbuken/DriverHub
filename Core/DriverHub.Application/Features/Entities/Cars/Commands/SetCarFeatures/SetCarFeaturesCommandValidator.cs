using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Cars.Commands.SetCarFeatures;

public sealed class SetCarFeaturesCommandValidator : AbstractValidator<SetCarFeaturesCommand>
{
    public SetCarFeaturesCommandValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Özellikleri güncellenecek aracın Id bilgisi boş bırakılamaz.");

        RuleForEach(x => x.FeatureIds)
            .ValidId("Feature Id bilgisi boş bırakılamaz.");

        RuleFor(x => x.FeatureIds)
            .Must(featureIds => featureIds.Distinct().Count() == featureIds.Count)
            .WithMessage("Aynı feature birden fazla kez gönderilemez.");
    }
}