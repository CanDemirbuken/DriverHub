using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.ContactFeatures.Queries.GetPagedContacts;

public sealed record GetPagedContactsQuery(int PageNumber, int PageSize) : IRequest<Result<PagedResponse<GetPagedContactsQueryResponse>>>;