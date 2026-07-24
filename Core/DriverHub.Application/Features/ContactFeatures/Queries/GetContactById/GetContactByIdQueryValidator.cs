using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;

public sealed class GetContactByIdQueryValidator: AbstractValidator<GetContactByIdQuery>
{
    public GetContactByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .ValidId("Getirilecek kaydın Id bilgisi boş bırakılamaz.");
    }
}