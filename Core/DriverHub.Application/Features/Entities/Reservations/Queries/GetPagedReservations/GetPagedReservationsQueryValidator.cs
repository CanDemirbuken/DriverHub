using FluentValidation;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetPagedReservations;

public sealed class GetPagedReservationsQueryValidator : AbstractValidator<GetPagedReservationsQuery>
{
    public GetPagedReservationsQueryValidator()
    {
        RuleFor(x => x.PageNumber).InclusiveBetween(1, 1000000);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        RuleFor(x => x.Status).IsInEnum().When(x => x.Status.HasValue);
        RuleFor(x => x.Search).MaximumLength(200);
        RuleFor(x => x.CarId).NotEqual(Guid.Empty).When(x => x.CarId.HasValue);
        RuleFor(x => x.To).GreaterThan(x => x.From).When(x => x.From.HasValue && x.To.HasValue);
    }
}
