namespace DriverHub.Application.Features.Entities.BannerFeatures.Queries.GetBannerById;

public sealed record GetBannerByIdQueryResponse(Guid Id, string Title, string Description, string VideoDescription, string VideoUrl);