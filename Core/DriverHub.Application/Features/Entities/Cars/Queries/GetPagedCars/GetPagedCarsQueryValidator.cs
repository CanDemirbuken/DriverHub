using FluentValidation;

namespace DriverHub.Application.Features.Entities.Cars.Queries.GetPagedCars;

public sealed class GetPagedCarsQueryValidator : AbstractValidator<GetPagedCarsQuery>
{
    public GetPagedCarsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Sayfa boyutu 1 ile 100 arasında olmalıdır.");
    }
}