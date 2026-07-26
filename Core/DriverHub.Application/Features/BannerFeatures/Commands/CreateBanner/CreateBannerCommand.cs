using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.BannerFeatures.Commands.CreateBanner;

public sealed record CreateBannerCommand(string Title, string Description, string VideoDescription, string VideoUrl) : IRequest<Result<CreateBannerCommandResponse>>;