using DriverHub.Domain.Enums;

namespace DriverHub.Application.Features.Entities.Reservations.Common;

public static class ReservationPriceCalculator
{
    public static ReservationPrice Calculate(
        int rentalDays,
        IEnumerable<(PricingType Type, decimal Amount)> carPricings,
        IEnumerable<decimal> extraDailyPrices,
        decimal insuranceDailyPrice)
    {
        int months = rentalDays / 30;
        int weeks = rentalDays % 30 / 7;
        int days = rentalDays % 7;

        IReadOnlyDictionary<PricingType, decimal> prices = carPricings
            .ToDictionary(item => item.Type, item => item.Amount);

        decimal basePrice = months * prices.GetValueOrDefault(PricingType.Monthly)
            + weeks * prices.GetValueOrDefault(PricingType.Weekly)
            + days * prices.GetValueOrDefault(PricingType.Daily);
        decimal extrasTotal = extraDailyPrices.Sum() * rentalDays;
        decimal insurancePrice = insuranceDailyPrice * rentalDays;

        return new ReservationPrice(rentalDays, basePrice, extrasTotal, insurancePrice, basePrice + extrasTotal + insurancePrice);
    }
}

public sealed record ReservationPrice(
    int RentalDays,
    decimal BasePrice,
    decimal ExtrasTotal,
    decimal InsurancePrice,
    decimal TotalPrice);
