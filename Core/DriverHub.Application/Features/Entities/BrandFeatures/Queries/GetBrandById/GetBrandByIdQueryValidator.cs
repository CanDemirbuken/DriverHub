using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.BrandFeatures.Queries.GetBrandById;

public sealed class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdQueryValidator()
    {
        RuleFor(b => b.Id)
            .ValidId("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}