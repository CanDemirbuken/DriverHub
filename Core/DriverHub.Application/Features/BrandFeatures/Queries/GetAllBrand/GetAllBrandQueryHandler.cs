using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetAllBrand;

public sealed class GetAllBrandQueryHandler(IBrandQueryService brandQueryService) : IRequestHandler<GetAllBrandQuery, IReadOnlyList<GetAllBrandQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllBrandQueryResponse>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
    {
        return await brandQueryService.GetAllAsync(cancellationToken);
    }
}