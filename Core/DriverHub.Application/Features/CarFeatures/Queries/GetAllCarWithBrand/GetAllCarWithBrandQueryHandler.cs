using DriverHub.Application.Features.CarFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetAllCarWithBrand;

public sealed class GetAllCarWithBrandQueryHandler(ICarRepository carRepository) : IRequestHandler<GetAllCarWithBrandQuery, IReadOnlyList<GetAllCarWithBrandQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllCarWithBrandQueryResponse>> Handle(GetAllCarWithBrandQuery request, CancellationToken cancellationToken)
    {
        var cars = await carRepository.GetAllCarWithBrandAsync(cancellationToken);

        IReadOnlyList<GetAllCarWithBrandQueryResponse> response = cars
            .Select(car => car.ToGetAllWithBrandResponse())
            .ToList();

        return response;
    }
}