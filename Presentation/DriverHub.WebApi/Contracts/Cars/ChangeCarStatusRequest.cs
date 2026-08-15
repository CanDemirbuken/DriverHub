using DriverHub.Domain.Enums;

namespace DriverHub.WebApi.Contracts.Cars;

public sealed record ChangeCarStatusRequest(CarStatus Status);