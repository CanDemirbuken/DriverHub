namespace DriverHub.Application.Features.BannerFeatures.Queries.GetBannerById;

public sealed record GetBannerByIdQueryResponse(Guid Id, string Title, string Description, string VideoDescription, string VideoUrl);