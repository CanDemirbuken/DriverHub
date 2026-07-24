using DriverHub.Application.Features.CarFeatures.Commands.UpdateCar;
using DriverHub.WebApi.Models.Cars;

namespace DriverHub.WebApi.Mappings;

public static class CarRequestMappings
{
    public static UpdateCarCommand ToCommand(this UpdateCarRequest request, Guid id)
    {
        return new UpdateCarCommand(
            id,
            request.BrandId,
            request.Model,
            request.CoverImageUrl,
            request.Km,
            request.Transmission,
            request.Seat,
            request.Luggage,
            request.Fuel,
            request.BigImageUrl
            );
    }
}