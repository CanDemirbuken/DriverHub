using AutoMapper;
using DriverHub.Application.Features.AboutFeatures.Commands.CreateAbout;
using DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;
using DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class AboutProfile : Profile
{
    public AboutProfile()
    {
        CreateMap<About, GetAboutByIdQueryResponse>();

        CreateMap<CreateAboutCommand, About>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());

        CreateMap<UpdateAboutCommand, About>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());
    }
}