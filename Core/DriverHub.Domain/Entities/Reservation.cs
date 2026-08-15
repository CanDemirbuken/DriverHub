using DriverHub.Domain.Abstraction;
using DriverHub.Domain.Enums;

namespace DriverHub.Domain.Entities;

public sealed class Reservation : Entity
{
    public string UserId { get; set; } = string.Empty;
    public Guid CarId { get; set; }
    public Car? Car { get; set; }
    public Guid PickupLocationId { get; set; }
    public Guid ReturnLocationId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ReservationStatus Status { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ExtraPrice { get; set; }
    public decimal InsurancePrice { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid? InsurancePackageId { get; set; }
    public InsurancePackage? InsurancePackage { get; set; }
    public ICollection<ReservationExtra> ReservationExtras { get; set; } = [];
}