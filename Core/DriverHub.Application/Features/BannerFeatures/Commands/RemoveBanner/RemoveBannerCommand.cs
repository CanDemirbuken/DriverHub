using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Commands.RemoveBanner;

public sealed record RemoveBannerCommand(Guid Id) : IRequest;