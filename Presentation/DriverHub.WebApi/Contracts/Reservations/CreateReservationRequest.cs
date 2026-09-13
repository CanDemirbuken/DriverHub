namespace DriverHub.WebApi.Contracts.Reservations;

public sealed record CreateReservationRequest(
    Guid CarId,
    Guid PickupLocationId,
    DateTime StartDate,
    DateTime EndDate,
    IReadOnlyCollection<Guid>? ExtraIds,
    Guid? InsurancePackageId);
