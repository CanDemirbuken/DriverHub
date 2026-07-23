namespace DriverHub.Application.Features.ContactFeatures.Queries.GetAllContact;

public sealed record GetAllContactQueryResponse(Guid Id, string Name, string Email, string Subject, string Message);