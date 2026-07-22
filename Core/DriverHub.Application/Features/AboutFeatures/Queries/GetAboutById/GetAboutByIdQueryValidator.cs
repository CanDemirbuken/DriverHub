using FluentValidation;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;

public sealed class GetAboutByIdQueryValidator : AbstractValidator<GetAboutByIdQuery>
{
    public GetAboutByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}