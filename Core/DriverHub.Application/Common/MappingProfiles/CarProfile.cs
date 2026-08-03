using AutoMapper;
using DriverHub.Application.Features.Entities.CarFeatures.Commands.CreateCar;
using DriverHub.Application.Features.Entities.CarFeatures.Commands.UpdateCar;
using DriverHub.Application.Features.Entities.CarFeatures.Queries.GetCarByIdWithBrand;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, GetCarByIdWithBrandQueryResponse>()
             .ForMember(
                 destination => destination.BrandName,
                 options => options.MapFrom(source => source.Brand.Name));

        CreateMap<CreateCarCommand, Car>()
           .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());

        CreateMap<UpdateCarCommand, Car>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore());
    }
}