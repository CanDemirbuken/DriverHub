using DriverHub.Application.Features.CarFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetAllCar;

public sealed class GetAllCarQueryHandler(IRepository<Car> repository) : IRequestHandler<GetAllCarQuery, IReadOnlyList<GetAllCarQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllCarQueryResponse>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
    {
        var cars = await repository.GetAllAsync(cancellationToken);

        IReadOnlyList<GetAllCarQueryResponse> response = cars
            .Select(car => car.ToGetAllResponse())
            .ToList();

        return response;
    }
}