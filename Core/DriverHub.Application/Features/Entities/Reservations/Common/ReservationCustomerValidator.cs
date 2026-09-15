using DriverHub.Application.Interfaces.QueryServices.Identity;
using FluentValidation;

namespace DriverHub.Application.Features.Entities.Reservations.Common;

public sealed class ReservationCustomerValidator : AbstractValidator<ReservationCustomer>
{
    public ReservationCustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.Phone).MaximumLength(50);
    }
}
