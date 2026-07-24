using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.CarFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed class GetCarByIdWithBrandQueryHandler(ICarRepository carRepository) : IRequestHandler<GetCarByIdWithBrandQuery, GetCarByIdWithBrandQueryResponse>
{
    public async Task<GetCarByIdWithBrandQueryResponse> Handle(GetCarByIdWithBrandQuery request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetCarByIdWithBrandAsync(request.Id, cancellationToken);
        if (car is null)
            throw new NotFoundException();

        GetCarByIdWithBrandQueryResponse response = car.ToGetByIdWithBrandResponse();
        return response;
    }
}