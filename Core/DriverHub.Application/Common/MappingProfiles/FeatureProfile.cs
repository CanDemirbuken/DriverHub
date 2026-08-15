using AutoMapper;
using DriverHub.Application.Features.Entities.Features.Commands.CreateFeature;
using DriverHub.Application.Features.Entities.Features.Commands.UpdateFeature;
using DriverHub.Application.Features.Entities.Features.Queries.GetAllFeature;
using DriverHub.Application.Features.Entities.Features.Queries.GetFeatureById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class FeatureProfile : Profile
{
    public FeatureProfile()
    {
        CreateMap<Feature, GetAllFeatureQueryResponse>();
        CreateMap<Feature, GetFeatureByIdQueryResponse>();

        CreateMap<CreateFeatureCommand, Feature>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore())
            .ForMember(destination => destination.CarFeatures, options => options.Ignore());

        CreateMap<UpdateFeatureCommand, Feature>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore())
            .ForMember(destination => destination.CarFeatures, options => options.Ignore());
    }
}