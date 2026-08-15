using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Queries.GetLocationById;

public sealed class GetLocationByIdQueryHandler(IRepository<Location> repository, IMapper mapper) : IRequestHandler<GetLocationByIdQuery, Result<GetLocationByIdQueryResponse>>
{
    public async Task<Result<GetLocationByIdQueryResponse>> Handle(
        GetLocationByIdQuery request,
        CancellationToken cancellationToken)
    {
        Location? location = await repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (location is null)
            return Result<GetLocationByIdQueryResponse>.Failure(
                Error.NotFound(
                    $"{request.Id} kimlik bilgisine sahip lokasyon bulunamadı.",
                    nameof(request.Id)));

        GetLocationByIdQueryResponse data =
            mapper.Map<GetLocationByIdQueryResponse>(location);

        return Result<GetLocationByIdQueryResponse>.Success(data);
    }
}