using DriverHub.Application.Features.BannerFeatures.Commands.CreateBanner;
using DriverHub.Application.Features.BannerFeatures.Commands.UpdateBanner;
using DriverHub.Application.Features.BannerFeatures.Queries.GetBannerById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Features.BannerFeatures.Mappings;

public static class BannerMappingExtensions
{
    public static GetBannerByIdQueryResponse ToGetByIdResponse(
        this Banner banner)
    {
        return new GetBannerByIdQueryResponse(
            banner.Id,
            banner.Title,
            banner.Description,
            banner.VideoDescription,
            banner.VideoUrl);
    }

    public static Banner ToEntity(this CreateBannerCommand command)
    {
        return new Banner
        {
            Title = command.Title,
            Description = command.Description,
            VideoDescription = command.VideoDescription,
            VideoUrl = command.VideoUrl
        };
    }

    public static void ApplyTo(
        this UpdateBannerCommand command,
        Banner banner)
    {
        banner.Title = command.Title;
        banner.Description = command.Description;
        banner.VideoDescription = command.VideoDescription;
        banner.VideoUrl = command.VideoUrl;
    }
}