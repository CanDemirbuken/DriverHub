using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.Entities.ContactFeatures.Queries.GetPagedContacts;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IContactQueryService
{
    Task<PagedResponse<GetPagedContactsQueryResponse>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}