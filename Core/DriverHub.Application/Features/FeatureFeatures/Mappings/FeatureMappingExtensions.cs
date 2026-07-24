using DriverHub.Application.Features.FeatureFeatures.Queries.GetFeatureById;
using DriverHub.Domain.Entities;

namespace DriverHub.Application.Features.FeatureFeatures.Mappings;

public static class FeatureMappingExtensions
{
    public static GetFeatureByIdQueryResponse ToGetByIdResponse(this Feature feature)
    {
        return new GetFeatureByIdQueryResponse
        (
            feature.Id,
            feature.Name
        );
    }
}
