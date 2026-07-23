using FluentValidation;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;

public sealed class GetPagedCarsWithBrandQueryValidator : AbstractValidator<GetPagedCarsWithBrandQuery>
{
    public GetPagedCarsWithBrandQueryValidator()
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