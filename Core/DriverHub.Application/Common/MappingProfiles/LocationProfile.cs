using AutoMapper;
using DriverHub.Application.Features.Entities.Locations.Commands.CreateLocation;
using DriverHub.Application.Features.Entities.Locations.Commands.UpdateLocation;
using DriverHub.Application.Features.Entities.Locations.Queries.GetAllLocation;
using DriverHub.Application.Features.Entities.Locations.Queries.GetLocationById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, GetAllLocationQueryResponse>();

        CreateMap<Location, GetLocationByIdQueryResponse>();

        CreateMap<CreateLocationCommand, Location>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore())
            .ForMember(destination => destination.Cars, options => options.Ignore());

        CreateMap<UpdateLocationCommand, Location>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore())
            .ForMember(destination => destination.Cars, options => options.Ignore());
    }
}