using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Features.Entities.Reservations.Commands.CreateReservation;
using DriverHub.Application.Features.Entities.Reservations.Commands.UpdateReservationStatus;
using DriverHub.Application.Features.Entities.Reservations.Queries.GetPagedReservations;
using DriverHub.Application.Interfaces.QueryServices.Identity;
using DriverHub.Application.Common.EmailTemplates;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;

namespace DriverHub.Tests;

public sealed class ReservationRulesTests
{
    [Theory]
    [InlineData(1, 1)] [InlineData(24, 1)] [InlineData(25, 2)] [InlineData(720, 30)]
    public void Rental_days_round_up_partial_days(int hours, int expected)
    {
        DateTime start = new(2030, 1, 1);
        Assert.Equal(expected, ReservationTimePolicy.GetRentalDays(start, start.AddHours(hours)));
    }

    [Theory]
    [InlineData(7, 600)] [InlineData(29, 2500)] [InlineData(30, 2000)]
    [InlineData(31, 2100)] [InlineData(37, 2600)] [InlineData(60, 4000)]
    public void Pricing_uses_month_then_week_then_remaining_days(int days, decimal expected)
    {
        var price = ReservationPriceCalculator.Calculate(days,
            [(PricingType.Daily, 100m), (PricingType.Weekly, 600m), (PricingType.Monthly, 2000m)], [10m], 20m);
        Assert.Equal(expected, price.BasePrice);
        Assert.Equal(expected + days * 30, price.TotalPrice);
    }

    [Fact]
    public void Invalid_create_and_customer_information_are_rejected()
    {
        var command = new CreateReservationCommand("", Guid.Empty, Guid.Empty, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-3), [], null);
        Assert.False(new CreateReservationCommandValidator().Validate(command).IsValid);
        Assert.False(new ReservationCustomerValidator().Validate(new ReservationCustomer("", "", "invalid", null)).IsValid);
        Assert.False(new UpdateReservationStatusCommandValidator().Validate(new UpdateReservationStatusCommand(Guid.NewGuid(), ReservationStatus.Completed, "admin")).IsValid);
        Assert.False(new GetPagedReservationsQueryValidator().Validate(new GetPagedReservationsQuery(PageSize: 101)).IsValid);
    }

    [Theory]
    [InlineData(ReservationStatus.Pending, "Pending")]
    [InlineData(ReservationStatus.Confirmed, "Approved")]
    [InlineData(ReservationStatus.Cancelled, "Cancelled")]
    public void Email_template_encodes_customer_content_and_includes_lifecycle(ReservationStatus status, string label)
    {
        var reservation = new Reservation { Status = status, CustomerEmail = "customer@example.test", CustomerFirstName = "<script>", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) };
        var mail = ReservationEmailTemplate.Create(reservation, "Car <b>", "Airport & City");
        Assert.Contains(label, mail.Body);
        Assert.Contains("&lt;script&gt;", mail.Body);
        Assert.DoesNotContain("<script>", mail.Body);
        Assert.Contains(reservation.Id.ToString(), mail.Body);
    }
}
