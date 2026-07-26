using DriverHub.Application.Features.CarFeatures.Commands.CreateCar;
using DriverHub.Application.Features.CarFeatures.Commands.UpdateCar;
using DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Features.CarFeatures.Mappings;

public static class CarMappingExtensions
{
    public static GetCarByIdWithBrandQueryResponse ToGetByIdWithBrandResponse(
        this Car car)
    {
        return new GetCarByIdWithBrandQueryResponse(
            car.Id,
            car.Brand!.Name,
            car.Model,
            car.CoverImageUrl,
            car.Km,
            car.Transmission,
            car.Seat,
            car.Luggage,
            car.Fuel,
            car.BigImageUrl);
    }

    public static Car ToEntity(this CreateCarCommand command)
    {
        return new Car
        {
            BrandId = command.BrandId,
            Model = command.Model,
            CoverImageUrl = command.CoverImageUrl,
            Km = command.Km,
            Transmission = command.Transmission,
            Seat = command.Seat,
            Luggage = command.Luggage,
            Fuel = command.Fuel,
            BigImageUrl = command.BigImageUrl
        };
    }

    public static void ApplyTo(this UpdateCarCommand command, Car car)
    {
        car.BrandId = command.BrandId;
        car.Model = command.Model;
        car.CoverImageUrl = command.CoverImageUrl;
        car.Km = command.Km;
        car.Transmission = command.Transmission;
        car.Seat = command.Seat;
        car.Luggage = command.Luggage;
        car.Fuel = command.Fuel;
        car.BigImageUrl = command.BigImageUrl;
    }
}