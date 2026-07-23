using FluentValidation;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetBrandById;

public sealed class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdQueryValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty()
            .WithMessage("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}
