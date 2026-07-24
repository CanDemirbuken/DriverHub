using DriverHub.Application.Common.Validations;
using FluentValidation;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetPagedContacts;

public sealed class GetPagedContactsQueryValidator : AbstractValidator<GetPagedContactsQuery>
{
    public GetPagedContactsQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .ValidPageNumber();

        RuleFor(query => query.PageSize)
            .ValidPageSize(50);
    }
}