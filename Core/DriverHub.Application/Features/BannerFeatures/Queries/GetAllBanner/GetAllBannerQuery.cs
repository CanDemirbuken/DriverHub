using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;

public sealed record GetAllBannerQuery : IRequest<IReadOnlyList<GetAllBannerQueryResponse>>;