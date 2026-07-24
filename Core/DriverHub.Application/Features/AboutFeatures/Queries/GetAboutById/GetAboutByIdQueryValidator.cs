using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;

public sealed class GetAboutByIdQueryValidator : AbstractValidator<GetAboutByIdQuery>
{
    public GetAboutByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .ValidId("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}