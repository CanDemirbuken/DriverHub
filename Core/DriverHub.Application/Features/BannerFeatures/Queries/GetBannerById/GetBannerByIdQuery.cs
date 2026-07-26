using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Queries.GetBannerById;

public sealed record GetBannerByIdQuery(Guid Id) : IRequest<Result<GetBannerByIdQueryResponse>>;