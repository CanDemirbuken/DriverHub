namespace DriverHub.Application.Features.ContactFeatures.Queries.GetContactById;

public sealed record GetContactByIdQueryResponse(Guid Id, string Name, string Email, string Subject, string Message);