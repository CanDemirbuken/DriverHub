using DriverHub.Application.Features.AboutFeatures.Commands.CreateAbout;
using DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;
using DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;
using DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Features.AboutFeatures.Mappings;

public static class AboutMappingExtensions
{
    public static GetAllAboutQueryResponse ToGetAllResponse(this About about)
    {
        return new GetAllAboutQueryResponse(
            about.Id,
            about.Title,
            about.Description,
            about.ImageUrl);
    }

    public static GetAboutByIdQueryResponse ToGetByIdResponse(this About about)
    {
        return new GetAboutByIdQueryResponse(
            about.Id,
            about.Title,
            about.Description,
            about.ImageUrl);
    }

    public static About ToEntity(this CreateAboutCommand command)
    {
        return new About
        {
            Title = command.Title,
            Description = command.Description,
            ImageUrl = command.ImageUrl
        };
    }

    public static void ApplyTo(this UpdateAboutCommand command, About about)
    {
        about.Title = command.Title;
        about.Description = command.Description;
        about.ImageUrl = command.ImageUrl;
    }
}