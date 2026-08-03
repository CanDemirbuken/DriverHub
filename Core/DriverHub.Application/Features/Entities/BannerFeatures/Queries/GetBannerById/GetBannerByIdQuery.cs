using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.BannerFeatures.Queries.GetBannerById;

public sealed record GetBannerByIdQuery(Guid Id) : IRequest<Result<GetBannerByIdQueryResponse>>;