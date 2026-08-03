using AutoMapper;
using DriverHub.Application.Features.Entities.BannerFeatures.Commands.CreateBanner;
using DriverHub.Application.Features.Entities.BannerFeatures.Commands.UpdateBanner;
using DriverHub.Application.Features.Entities.BannerFeatures.Queries.GetBannerById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class BannerProfile : Profile
{
    public BannerProfile()
    {
        CreateMap<Banner, GetBannerByIdQueryResponse>();

        CreateMap<CreateBannerCommand, Banner>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());

        CreateMap<UpdateBannerCommand, Banner>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());
    }
}