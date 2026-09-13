using DriverHub.Domain.Enums;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetReservationQuote;

public sealed record GetReservationQuoteResponse(
    ReservationQuoteCarInfo Car,
    Guid PickupLocationId,
    string PickupLocationName,
    Guid ReturnLocationId,
    string ReturnLocationName,
    DateTime StartDate,
    DateTime EndDate,
    int RentalDays,
    decimal BasePrice,
    IReadOnlyCollection<ReservationQuoteExtraInfo> SelectedExtras,
    IReadOnlyCollection<ReservationQuoteExtraInfo> AvailableExtras,
    ReservationQuoteInsuranceInfo? SelectedInsurance,
    IReadOnlyCollection<ReservationQuoteInsuranceInfo> AvailableInsurancePackages,
    decimal ExtrasTotal,
    decimal InsurancePrice,
    decimal TotalPrice);

public sealed record ReservationQuoteCarInfo(Guid Id, string BrandName, string Model, short ModelYear, string Plate, string CoverImageUrl, CarStatus Status);
public sealed record ReservationQuoteExtraInfo(Guid Id, string Name, decimal DailyPrice);
public sealed record ReservationQuoteInsuranceInfo(Guid Id, string Name, decimal DailyPrice);
