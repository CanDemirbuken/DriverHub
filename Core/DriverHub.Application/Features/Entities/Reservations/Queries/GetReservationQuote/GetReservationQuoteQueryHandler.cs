using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Cars.Queries.GetCarById;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetReservationQuote;

public sealed class GetReservationQuoteQueryHandler(
    ICarQueryService carQueryService,
    IReservationQueryService reservationQueryService,
    IRepository<Extra> extraRepository,
    IRepository<InsurancePackage> insuranceRepository) : IRequestHandler<GetReservationQuoteQuery, Result<GetReservationQuoteResponse>>
{
    public async Task<Result<GetReservationQuoteResponse>> Handle(GetReservationQuoteQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Guid> extraIds = request.ExtraIds ?? [];
        GetCarByIdQueryResponse? car = await carQueryService.GetByIdAsync(request.CarId, cancellationToken);
        if (car is null)
            return Result<GetReservationQuoteResponse>.Failure(Error.NotFound("Araç bulunamadı.", nameof(request.CarId)));
        if (car.Status != CarStatus.Active || car.CurrentLocationId != request.PickupLocationId)
            return Result<GetReservationQuoteResponse>.Failure(Error.Conflict("Seçilen araç teslim alma lokasyonunda kiralamaya uygun değil.", nameof(request.CarId)));
        if (car.Pricings.Count != Enum.GetValues<PricingType>().Length)
            return Result<GetReservationQuoteResponse>.Failure(Error.Failure("Araç için eksik fiyat bilgisi bulundu.", nameof(request.CarId)));
        if (!await reservationQueryService.LocationExistsAsync(request.PickupLocationId, cancellationToken))
            return Result<GetReservationQuoteResponse>.Failure(Error.NotFound("Teslim alma lokasyonu bulunamadı.", nameof(request.PickupLocationId)));
        if (!await reservationQueryService.IsCarAvailableAsync(request.CarId, request.StartDate, request.EndDate, cancellationToken))
            return Result<GetReservationQuoteResponse>.Failure(Error.Conflict("Seçilen araç bu tarih aralığında artık müsait değil.", nameof(request.CarId)));

        IReadOnlyList<Extra> extras = await extraRepository.GetAllAsync(cancellationToken);
        IReadOnlyList<InsurancePackage> insurances = await insuranceRepository.GetAllAsync(cancellationToken);
        if (extraIds.Distinct().Count() != extraIds.Count || extraIds.Any(id => extras.All(extra => extra.Id != id)))
            return Result<GetReservationQuoteResponse>.Failure(Error.Validation("Reservation.InvalidExtra", "Seçilen ek hizmetlerden biri geçersiz.", nameof(request.ExtraIds)));
        InsurancePackage? insurance = request.InsurancePackageId is null ? null : insurances.FirstOrDefault(item => item.Id == request.InsurancePackageId);
        if (request.InsurancePackageId is not null && insurance is null)
            return Result<GetReservationQuoteResponse>.Failure(Error.Validation("Reservation.InvalidInsurance", "Seçilen sigorta paketi geçersiz.", nameof(request.InsurancePackageId)));

        int rentalDays = ReservationTimePolicy.GetRentalDays(request.StartDate, request.EndDate);
        ReservationPrice price = ReservationPriceCalculator.Calculate(
            rentalDays,
            car.Pricings.Select(item => (item.Type, item.Amount)),
            extras.Where(item => extraIds.Contains(item.Id)).Select(item => item.DailyPrice),
            insurance?.DailyPrice ?? 0);

        return Result<GetReservationQuoteResponse>.Success(new GetReservationQuoteResponse(
            new ReservationQuoteCarInfo(car.Id, car.BrandName, car.Model, car.ModelYear, car.Plate, car.CoverImageUrl, car.Status),
            request.PickupLocationId, car.CurrentLocationName, request.PickupLocationId, car.CurrentLocationName,
            request.StartDate, request.EndDate, price.RentalDays, price.BasePrice,
            extras.Where(item => extraIds.Contains(item.Id)).Select(item => new ReservationQuoteExtraInfo(item.Id, item.Name, item.DailyPrice)).ToArray(),
            extras.Select(item => new ReservationQuoteExtraInfo(item.Id, item.Name, item.DailyPrice)).ToArray(),
            insurance is null ? null : new ReservationQuoteInsuranceInfo(insurance.Id, insurance.Name, insurance.DailyPrice),
            insurances.Select(item => new ReservationQuoteInsuranceInfo(item.Id, item.Name, item.DailyPrice)).ToArray(),
            price.ExtrasTotal, price.InsurancePrice, price.TotalPrice));
    }
}
