namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;

public sealed record GetAllAboutQueryResponse(Guid Id, string Title, string Description, string ImageUrl);