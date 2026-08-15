using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Features.Queries.GetFeatureById;

public sealed class GetFeatureByIdQueryHandler(IRepository<Feature> repository, IMapper mapper) : IRequestHandler<GetFeatureByIdQuery, Result<GetFeatureByIdQueryResponse>>
{
    public async Task<Result<GetFeatureByIdQueryResponse>> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
    {
        Feature? feature = await repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (feature is null)
            return Result<GetFeatureByIdQueryResponse>.Failure(
                Error.NotFound(
                    $"{request.Id} kimlik bilgisine sahip özellik bulunamadı.",
                    nameof(request.Id)));

        GetFeatureByIdQueryResponse data =
            mapper.Map<GetFeatureByIdQueryResponse>(feature);

        return Result<GetFeatureByIdQueryResponse>.Success(data);
    }
}