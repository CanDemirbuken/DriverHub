namespace DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;

public sealed record GetAllBannerQueryResponse(Guid Id, string Title, string FormattedDescription);