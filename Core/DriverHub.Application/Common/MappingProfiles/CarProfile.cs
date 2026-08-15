using AutoMapper;
using DriverHub.Application.Features.Entities.Cars.Commands.CreateCar;
using DriverHub.Application.Features.Entities.Cars.Commands.UpdateCar;

namespace DriverHub.Application.Common.MappingProfiles;

public sealed class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CreateCarCommand, Car>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore())
            .ForMember(destination => destination.Status, options => options.Ignore())
            .ForMember(destination => destination.Brand, options => options.Ignore())
            .ForMember(destination => destination.Category, options => options.Ignore())
            .ForMember(destination => destination.CurrentLocation, options => options.Ignore())
            .ForMember(destination => destination.CarFeatures, options => options.Ignore())
            .ForMember(destination => destination.CarDescription, options => options.Ignore())
            .ForMember(destination => destination.CarPricings, options => options.Ignore())
            .ForMember(destination => destination.Reservations, options => options.Ignore());

        CreateMap<UpdateCarCommand, Car>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedDate, options => options.Ignore())
            .ForMember(destination => destination.UpdatedDate, options => options.Ignore())
            .ForMember(destination => destination.Status, options => options.Ignore())
            .ForMember(destination => destination.Brand, options => options.Ignore())
            .ForMember(destination => destination.Category, options => options.Ignore())
            .ForMember(destination => destination.CurrentLocation, options => options.Ignore())
            .ForMember(destination => destination.CarFeatures, options => options.Ignore())
            .ForMember(destination => destination.CarDescription, options => options.Ignore())
            .ForMember(destination => destination.CarPricings, options => options.Ignore())
            .ForMember(destination => destination.Reservations, options => options.Ignore());
    }
}