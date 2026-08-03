using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using MediatR;

namespace DriverHub.Application.Features.Entities.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed class GetCarByIdWithBrandQueryHandler(ICarRepository carRepository, IMapper mapper) : IRequestHandler<GetCarByIdWithBrandQuery, Result<GetCarByIdWithBrandQueryResponse>>
{
    public async Task<Result<GetCarByIdWithBrandQueryResponse>> Handle(GetCarByIdWithBrandQuery request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetCarByIdWithBrandAsync(request.Id, cancellationToken);
        if (car is null)
            return Result<GetCarByIdWithBrandQueryResponse>.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        GetCarByIdWithBrandQueryResponse data = mapper.Map<GetCarByIdWithBrandQueryResponse>(car);
        return Result<GetCarByIdWithBrandQueryResponse>.Success(data);
    }
}