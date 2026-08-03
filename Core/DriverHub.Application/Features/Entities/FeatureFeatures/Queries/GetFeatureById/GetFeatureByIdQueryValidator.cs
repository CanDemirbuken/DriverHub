using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.FeatureFeatures.Queries.GetFeatureById;

public sealed class GetFeatureByIdQueryValidator : AbstractValidator<GetFeatureByIdQuery>
{
    public GetFeatureByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .ValidId("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}
