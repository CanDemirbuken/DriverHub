namespace DriverHub.Application.Features.Entities.AboutFeatures.Queries.GetAboutById;

public sealed record GetAboutByIdQueryResponse(Guid Id, string Title, string Description, string ImageUrl);