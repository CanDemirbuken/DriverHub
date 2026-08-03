using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.BannerFeatures.Commands.RemoveBanner;

public sealed record RemoveBannerCommand(Guid Id) : IRequest<Result>;