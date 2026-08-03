using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.Entities.ContactFeatures.Queries.GetPagedContacts;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class ContactQueryService(AppDbContext context) : IContactQueryService
{
    public async Task<PagedResponse<GetPagedContactsQueryResponse>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        IQueryable<Contact> query = context
            .Set<Contact>()
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(contact => contact.CreatedDate)
            .ThenByDescending(contact => contact.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(contact => new GetPagedContactsQueryResponse(
                contact.Id,
                contact.Name,
                contact.Email,
                contact.Subject))
            .ToListAsync(cancellationToken);

        return PagedResponse<GetPagedContactsQueryResponse>.CreateResponse(items, pageNumber, pageSize, totalCount);
    }
}