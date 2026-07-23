using FluentValidation;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed class GetCarByIdWithBrandQueryValidator : AbstractValidator<GetCarByIdWithBrandQuery>
{
    public GetCarByIdWithBrandQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}