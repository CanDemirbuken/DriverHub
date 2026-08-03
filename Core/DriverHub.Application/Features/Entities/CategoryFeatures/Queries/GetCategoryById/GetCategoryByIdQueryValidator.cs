using FluentValidation;

namespace DriverHub.Application.Features.Entities.CategoryFeatures.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}