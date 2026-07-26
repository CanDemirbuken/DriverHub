using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.ContactFeatures.Queries.GetPagedContacts;

public sealed class GetPagedContactsQueryHandler(IContactQueryService contactQueryService) : IRequestHandler<GetPagedContactsQuery, Result<PagedResponse<GetPagedContactsQueryResponse>>>
{
    public async Task<Result<PagedResponse<GetPagedContactsQueryResponse>>> Handle(GetPagedContactsQuery request, CancellationToken cancellationToken)
    {
        var data = await contactQueryService.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        return Result<PagedResponse<GetPagedContactsQueryResponse>>.Success(data, StatusCodes.Status200OK);
    }
}