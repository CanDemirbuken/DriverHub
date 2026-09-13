using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Cars.Queries.GetCarById;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Commands.CreateReservation;

public sealed class CreateReservationCommandHandler(
    IRepository<Car> carRepository,
    IRepository<Location> locationRepository,
    IRepository<Reservation> reservationRepository,
    IRepository<Extra> extraRepository,
    IRepository<InsurancePackage> insuranceRepository,
    IRepository<ReservationExtra> reservationExtraRepository,
    ICarQueryService carQueryService,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateReservationCommand, Result<CreateReservationCommandResponse>>
{
    public async Task<Result<CreateReservationCommandResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        Car? car = await carRepository.GetByIdAsync(request.CarId, cancellationToken);

        if (car is null)
            return Result<CreateReservationCommandResponse>.Failure(Error.NotFound("Araç bulunamadı.", nameof(request.CarId)));

        if (car.Status != CarStatus.Active)
            return Result<CreateReservationCommandResponse>.Failure(Error.Conflict("Seçilen araç şu anda kiralamaya uygun değil.", nameof(request.CarId)));

        bool pickupLocationExists = await locationRepository.AnyAsync(
            location => location.Id == request.PickupLocationId,
            cancellationToken);

        if (!pickupLocationExists)
            return Result<CreateReservationCommandResponse>.Failure(Error.NotFound("Teslim alma lokasyonu bulunamadı.", nameof(request.PickupLocationId)));

        if (car.CurrentLocationId != request.PickupLocationId)
            return Result<CreateReservationCommandResponse>.Failure(Error.Conflict("Araç seçilen teslim alma lokasyonunda bulunmuyor.", nameof(request.PickupLocationId)));

        GetCarByIdQueryResponse? carDetails = await carQueryService.GetByIdAsync(request.CarId, cancellationToken);
        if (carDetails is null || carDetails.Pricings.Count != Enum.GetValues<PricingType>().Length)
            return Result<CreateReservationCommandResponse>.Failure(Error.Failure("Araç için eksik fiyat bilgisi bulundu.", nameof(request.CarId)));
        IReadOnlyList<Extra> extras = await extraRepository.GetAllAsync(cancellationToken);
        IReadOnlyList<InsurancePackage> insurances = await insuranceRepository.GetAllAsync(cancellationToken);
        if (request.ExtraIds.Any(id => extras.All(extra => extra.Id != id)))
            return Result<CreateReservationCommandResponse>.Failure(Error.Validation("Reservation.InvalidExtra", "Seçilen ek hizmetlerden biri geçersiz.", nameof(request.ExtraIds)));
        InsurancePackage? insurance = request.InsurancePackageId is null ? null : insurances.FirstOrDefault(item => item.Id == request.InsurancePackageId);
        if (request.InsurancePackageId is not null && insurance is null)
            return Result<CreateReservationCommandResponse>.Failure(Error.Validation("Reservation.InvalidInsurance", "Seçilen sigorta paketi geçersiz.", nameof(request.InsurancePackageId)));

        int rentalDays = Math.Max(1, (int)Math.Ceiling((request.EndDate - request.StartDate).TotalDays));
        ReservationPrice price = ReservationPriceCalculator.Calculate(
            rentalDays,
            carDetails.Pricings.Select(item => (item.Type, item.Amount)),
            extras.Where(item => request.ExtraIds.Contains(item.Id)).Select(item => item.DailyPrice),
            insurance?.DailyPrice ?? 0);

        bool overlaps = await reservationRepository.AnyAsync(
            reservation => reservation.CarId == request.CarId &&
                (reservation.Status == ReservationStatus.Pending || reservation.Status == ReservationStatus.Confirmed) &&
                reservation.StartDate < request.EndDate &&
                reservation.EndDate > request.StartDate,
            cancellationToken);

        if (overlaps)
            return Result<CreateReservationCommandResponse>.Failure(Error.Conflict("Araç seçilen tarih aralığında başka bir rezervasyonla çakışıyor.", nameof(request.CarId)));

        await using IUnitOfWorkTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        bool finalOverlap = await reservationRepository.AnyAsync(
            reservation => reservation.CarId == request.CarId &&
                (reservation.Status == ReservationStatus.Pending || reservation.Status == ReservationStatus.Confirmed) &&
                reservation.StartDate < request.EndDate &&
                reservation.EndDate > request.StartDate,
            cancellationToken);

        if (finalOverlap)
            return Result<CreateReservationCommandResponse>.Failure(Error.Conflict("Araç seçilen tarih aralığında başka bir rezervasyonla çakışıyor.", nameof(request.CarId)));

        Reservation reservation = new()
        {
            UserId = request.UserId,
            CarId = request.CarId,
            PickupLocationId = request.PickupLocationId,
            ReturnLocationId = request.PickupLocationId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = ReservationStatus.Pending,
            BasePrice = price.BasePrice,
            ExtraPrice = price.ExtrasTotal,
            InsurancePrice = price.InsurancePrice,
            TotalPrice = price.TotalPrice,
            InsurancePackageId = insurance?.Id
        };

        await reservationRepository.AddAsync(reservation, cancellationToken);
        foreach (Extra extra in extras.Where(item => request.ExtraIds.Contains(item.Id)))
            await reservationExtraRepository.AddAsync(new ReservationExtra
            {
                ReservationId = reservation.Id,
                ExtraId = extra.Id,
                UnitPrice = extra.DailyPrice,
                TotalPrice = extra.DailyPrice * price.RentalDays
            }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return Result<CreateReservationCommandResponse>.Success(new CreateReservationCommandResponse(reservation.Id, price.BasePrice, price.ExtrasTotal, price.InsurancePrice, price.TotalPrice));
    }
}
