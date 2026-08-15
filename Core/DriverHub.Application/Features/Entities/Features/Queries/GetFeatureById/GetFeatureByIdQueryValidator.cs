using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Features.Queries.GetFeatureById;

public sealed class GetFeatureByIdQueryValidator : AbstractValidator<GetFeatureByIdQuery>
{
    public GetFeatureByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .ValidId("Özellik Id bilgisi boş bırakılamaz.");
    }
}