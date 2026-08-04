using FluentValidation;

namespace DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetRoleById;

public sealed class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Getirilecek rolün Id bilgisi boş bırakılamaz.");
    }
}