using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.CarFeatures.Queries.GetPagedCarsWithBrand;

public sealed class GetPagedCarsWithBrandQueryValidator : AbstractValidator<GetPagedCarsWithBrandQuery>
{
    public GetPagedCarsWithBrandQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .ValidPageNumber();

        RuleFor(query => query.PageSize)
            .ValidPageSize(100);
    }
}