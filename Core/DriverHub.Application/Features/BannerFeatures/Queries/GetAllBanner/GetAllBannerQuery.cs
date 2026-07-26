using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;

public sealed record GetAllBannerQuery : IRequest<Result<IReadOnlyList<GetAllBannerQueryResponse>>>;