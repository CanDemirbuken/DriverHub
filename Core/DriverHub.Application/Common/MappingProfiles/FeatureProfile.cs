using AutoMapper;
using DriverHub.Application.Features.FeatureFeatures.Commands.CreateFeature;
using DriverHub.Application.Features.FeatureFeatures.Commands.UpdateFeature;
using DriverHub.Application.Features.FeatureFeatures.Queries.GetFeatureById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class FeatureProfile : Profile
{
    public FeatureProfile()
    {
        CreateMap<Feature, GetFeatureByIdQueryResponse>();

        CreateMap<CreateFeatureCommand, Feature>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());

        CreateMap<UpdateFeatureCommand, Feature>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());
    }
}
