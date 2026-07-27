using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;

public sealed class GetCarByIdWithBrandQueryHandler(ICarRepository carRepository, IMapper mapper) : IRequestHandler<GetCarByIdWithBrandQuery, Result<GetCarByIdWithBrandQueryResponse>>
{
    public async Task<Result<GetCarByIdWithBrandQueryResponse>> Handle(GetCarByIdWithBrandQuery request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetCarByIdWithBrandAsync(request.Id, cancellationToken);
        if (car is null)
            return Result<GetCarByIdWithBrandQueryResponse>.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.");

        GetCarByIdWithBrandQueryResponse data = mapper.Map<GetCarByIdWithBrandQueryResponse>(car);
        return Result<GetCarByIdWithBrandQueryResponse>.Success(data, StatusCodes.Status200OK);
    }
}