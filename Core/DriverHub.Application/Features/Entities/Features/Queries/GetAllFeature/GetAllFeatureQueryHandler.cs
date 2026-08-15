using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Features.Queries.GetAllFeature;

public sealed class GetAllFeatureQueryHandler(IRepository<Feature> repository, IMapper mapper) : IRequestHandler<GetAllFeatureQuery, Result<IReadOnlyList<GetAllFeatureQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllFeatureQueryResponse>>> Handle(
        GetAllFeatureQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Feature> features =
            await repository.GetAllAsync(cancellationToken);

        IReadOnlyList<GetAllFeatureQueryResponse> data =
            mapper.Map<IReadOnlyList<GetAllFeatureQueryResponse>>(features);

        return Result<IReadOnlyList<GetAllFeatureQueryResponse>>.Success(data);
    }
}