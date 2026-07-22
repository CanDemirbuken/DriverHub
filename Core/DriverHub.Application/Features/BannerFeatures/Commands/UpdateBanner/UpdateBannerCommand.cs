using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Commands.UpdateBanner;

public sealed record UpdateBannerCommand(Guid Id, string Title, string Description, string VideoDescription, string VideoUrl) : IRequest;