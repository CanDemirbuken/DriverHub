namespace DriverHub.Application.Features.Entities.ContactFeatures.Queries.GetPagedContacts;

public sealed record GetPagedContactsQueryResponse(Guid Id, string Name, string Email, string Subject);