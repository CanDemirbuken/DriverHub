using FluentValidation;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;

public sealed class GetContactByIdQueryValidator: AbstractValidator<GetContactByIdQuery>
{
    public GetContactByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}