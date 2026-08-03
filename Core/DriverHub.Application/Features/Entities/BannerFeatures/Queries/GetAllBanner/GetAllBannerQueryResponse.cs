namespace DriverHub.Application.Features.Entities.BannerFeatures.Queries.GetAllBanner;

public sealed record GetAllBannerQueryResponse(Guid Id, string Title, string FormattedDescription);