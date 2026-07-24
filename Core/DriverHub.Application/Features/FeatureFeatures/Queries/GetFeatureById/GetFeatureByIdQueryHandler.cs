using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.FeatureFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.FeatureFeatures.Queries.GetFeatureById;

public sealed class GetFeatureByIdQueryHandler(IRepository<Feature> featureRepository) : IRequestHandler<GetFeatureByIdQuery, GetFeatureByIdQueryResponse>
{
    public async Task<GetFeatureByIdQueryResponse> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
    {
        var feature = await featureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (feature is null)
            throw new NotFoundException();

        return feature.ToGetByIdResponse();
    }
}
