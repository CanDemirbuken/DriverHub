using FluentValidation;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCars;

public sealed class GetPagedCarsQueryValidator : AbstractValidator<GetPagedCarsQuery>
{
    public GetPagedCarsQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .NotEmpty()
            .WithMessage("Page number is required.")
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(query => query.PageSize)
            .NotEmpty()
            .WithMessage("Page size is required.")
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size cannot exceed 100.");
    }
}