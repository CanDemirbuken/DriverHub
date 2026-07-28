using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.FeatureFeatures.Queries.GetFeatureById;

public sealed class GetFeatureByIdQueryHandler(IRepository<Feature> featureRepository, IMapper mapper) : IRequestHandler<GetFeatureByIdQuery, Result<GetFeatureByIdQueryResponse>>
{
    public async Task<Result<GetFeatureByIdQueryResponse>> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
    {
        var feature = await featureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (feature is null)
            return Result<GetFeatureByIdQueryResponse>.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip kayıt bulunamadı.", nameof(request.Id)));

        var data = mapper.Map<GetFeatureByIdQueryResponse>(feature);
        return Result<GetFeatureByIdQueryResponse>.Success(data);
    }
}