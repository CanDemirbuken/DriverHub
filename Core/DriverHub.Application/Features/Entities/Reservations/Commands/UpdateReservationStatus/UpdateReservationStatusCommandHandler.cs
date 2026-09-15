using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Interfaces.Communication;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Application.Interfaces.QueryServices.Identity;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Commands.UpdateReservationStatus;

public sealed class UpdateReservationStatusCommandHandler(
    IReservationRepository repository, IReservationQueryService queryService,
    IUserQueryService userQueryService, IReservationNotificationQueue notificationQueue,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateReservationStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
    {
        if (await userQueryService.GetReservationCustomerAsync(request.ActorId, cancellationToken) is null)
            return Result.Failure(Error.Unauthorized("Geçerli kullanıcı bulunamadı."));
        ReservationResponse? details = await queryService.GetByIdAsync(request.Id, cancellationToken);
        if (details is null) return Result.Failure(Error.NotFound("Rezervasyon bulunamadı."));

        await using IUnitOfWorkTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        Car? car = await repository.GetCarForUpdateAsync(details.CarId, cancellationToken);
        Reservation? reservation = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (reservation is null) return Result.Failure(Error.NotFound("Rezervasyon bulunamadı."));
        // A repeated successful operation never rewrites audit fields or enqueues another email.
        if (reservation.Status == request.Status) return Result.Success();
        if (reservation.Status != ReservationStatus.Pending)
            return Result.Failure(Error.Conflict("Yalnızca bekleyen rezervasyonlar onaylanabilir veya iptal edilebilir."));

        if (request.Status == ReservationStatus.Confirmed)
        {
            if (!ReservationTimePolicy.IsStartAllowed(reservation.StartDate) || reservation.EndDate <= reservation.StartDate)
                return Result.Failure(Error.Conflict("Rezervasyonun tarih aralığı artık geçerli değil."));
            if (car is null || car.Status != CarStatus.Active || car.CurrentLocationId != reservation.PickupLocationId)
                return Result.Failure(Error.Conflict("Araç teslim alma lokasyonunda kiralamaya uygun değil."));
            bool overlap = await repository.AnyAsync(other => other.Id != reservation.Id &&
                other.CarId == reservation.CarId &&
                (other.Status == ReservationStatus.Pending || other.Status == ReservationStatus.Confirmed) &&
                other.StartDate < reservation.EndDate && other.EndDate > reservation.StartDate, cancellationToken);
            if (overlap) return Result.Failure(Error.Conflict("Araç bu tarih aralığında başka bir rezervasyonla çakışıyor."));
        }

        reservation.Status = request.Status;
        reservation.ProcessedAt = DateTime.UtcNow;
        reservation.ProcessedBy = request.ActorId;
        await notificationQueue.EnqueueAsync(reservation, $"{details.BrandName} {details.Model} — {details.Plate}", details.PickupLocationName, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return Result.Success();
    }
}
