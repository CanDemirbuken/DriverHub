using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Domain.Enums;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetPagedReservations;

public sealed record GetPagedReservationsQuery(
    int PageNumber = 1, int PageSize = 10, ReservationStatus? Status = null,
    string? Search = null, Guid? CarId = null, DateTime? From = null,
    DateTime? To = null, bool OldestFirst = false) : IRequest<Result<PagedResponse<ReservationResponse>>>;
