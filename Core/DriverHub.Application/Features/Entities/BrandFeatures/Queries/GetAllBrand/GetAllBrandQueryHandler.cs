using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.Entities.BrandFeatures.Queries.GetAllBrand;

public sealed class GetAllBrandQueryHandler(IBrandQueryService brandQueryService) : IRequestHandler<GetAllBrandQuery, Result<IReadOnlyList<GetAllBrandQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllBrandQueryResponse>>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
    {
        var data = await brandQueryService.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllBrandQueryResponse>>.Success(data);
    }
}