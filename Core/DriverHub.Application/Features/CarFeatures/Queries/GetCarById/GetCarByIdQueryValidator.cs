using FluentValidation;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarById;

public sealed class GetCarByIdQueryValidator : AbstractValidator<GetCarByIdQuery>
{
    public GetCarByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}