using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed class GetCarByIdWithBrandQueryValidator : AbstractValidator<GetCarByIdWithBrandQuery>
{
    public GetCarByIdWithBrandQueryValidator()
    {
        RuleFor(query => query.Id)
            .ValidId("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}