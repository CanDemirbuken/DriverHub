using FluentValidation;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetBrandById;

public sealed class GetBrandByIdValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty()
            .WithMessage("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}
