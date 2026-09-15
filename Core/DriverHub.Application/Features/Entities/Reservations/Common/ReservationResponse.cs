using DriverHub.Domain.Enums;

namespace DriverHub.Application.Features.Entities.Reservations.Common;

public sealed record ReservationResponse(
    Guid Id, Guid CarId, string BrandName, string Model, string Plate,
    string? CustomerFirstName, string? CustomerLastName, string? CustomerEmail, string? CustomerPhone,
    Guid PickupLocationId, string PickupLocationName, Guid ReturnLocationId, string ReturnLocationName,
    DateTime StartDate, DateTime EndDate, DateTime CreatedDate, ReservationStatus Status,
    DateTime? ProcessedAt, string? ProcessedByName,
    decimal BasePrice, decimal ExtraPrice, decimal InsurancePrice, decimal TotalPrice)
{
    public int RentalDays => ReservationTimePolicy.GetRentalDays(StartDate, EndDate);
}
