using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarById;

public sealed class GetCarByIdQueryValidator : AbstractValidator<GetCarByIdQuery>
{
    public GetCarByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .ValidId("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}