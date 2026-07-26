using DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;
using DriverHub.WebApi.Models.Abouts;

namespace DriverHub.WebApi.Mappings;

public static class AboutMappingExtensions
{
    public static UpdateAboutCommand ToCommand(
        this UpdateAboutRequest request,
        Guid id)
    {
        return new UpdateAboutCommand(
            id,
            request.Title,
            request.Description,
            request.ImageUrl);
    }
}