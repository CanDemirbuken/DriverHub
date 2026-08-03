namespace DriverHub.Application.Features.Entities.AboutFeatures.Queries.GetAllAbout;

public sealed record GetAllAboutQueryResponse(Guid Id, string Title, string FormattedDescription, string ImageUrl);