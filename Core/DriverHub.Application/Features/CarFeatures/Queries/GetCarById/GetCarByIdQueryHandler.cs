using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.CarFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarById;

public sealed class GetCarByIdQueryHandler(IRepository<Car> repository) : IRequestHandler<GetCarByIdQuery, GetCarByIdQueryResponse>
{
    public async Task<GetCarByIdQueryResponse> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var car = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (car is null)
            throw new NotFoundException();

        GetCarByIdQueryResponse response = car.ToGetByIdResponse();
        return response;
    }
}