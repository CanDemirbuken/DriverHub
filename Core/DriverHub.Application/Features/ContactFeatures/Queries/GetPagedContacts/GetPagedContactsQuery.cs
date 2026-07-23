using DriverHub.Application.Common.Models;
using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetPagedContacts;

public sealed record GetPagedContactsQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<GetPagedContactsQueryResponse>>;