using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetBannerById;

public sealed class GetBannerByIdQueryValidator : AbstractValidator<GetBannerByIdQuery>
{
    public GetBannerByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .ValidId("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}