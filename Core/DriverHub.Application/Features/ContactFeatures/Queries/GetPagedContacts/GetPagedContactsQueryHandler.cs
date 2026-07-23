using DriverHub.Application.Common.Models;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetPagedContacts;

public sealed class GetPagedContactsQueryHandler(IContactQueryService contactQueryService) : IRequestHandler<GetPagedContactsQuery, PagedResponse<GetPagedContactsQueryResponse>>
{
    public async Task<PagedResponse<GetPagedContactsQueryResponse>> Handle(GetPagedContactsQuery request, CancellationToken cancellationToken)
    {
        return await contactQueryService.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}