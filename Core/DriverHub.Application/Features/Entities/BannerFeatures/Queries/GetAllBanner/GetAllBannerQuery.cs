using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.BannerFeatures.Queries.GetAllBanner;

public sealed record GetAllBannerQuery : IRequest<Result<IReadOnlyList<GetAllBannerQueryResponse>>>;