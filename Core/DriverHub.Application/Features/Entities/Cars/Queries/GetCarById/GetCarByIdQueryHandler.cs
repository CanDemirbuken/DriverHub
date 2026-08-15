using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Queries.GetCarById;

public sealed class GetCarByIdQueryHandler(ICarQueryService carQueryService) : IRequestHandler<GetCarByIdQuery, Result<GetCarByIdQueryResponse>>
{
    public async Task<Result<GetCarByIdQueryResponse>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        GetCarByIdQueryResponse? car = await carQueryService.GetByIdAsync(request.Id, cancellationToken);

        if (car is null)
            return Result<GetCarByIdQueryResponse>.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        return Result<GetCarByIdQueryResponse>.Success(car);
    }
}