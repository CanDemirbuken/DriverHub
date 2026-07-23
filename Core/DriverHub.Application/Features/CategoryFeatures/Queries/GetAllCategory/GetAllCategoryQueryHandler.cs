using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Queries.GetAllCategory;

public sealed class GetAllCategoryQueryHandler(ICategoryQueryService categoryQueryService) : IRequestHandler<GetAllCategoryQuery, IReadOnlyList<GetAllCategoryQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllCategoryQueryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        return await categoryQueryService.GetAllAsync(cancellationToken);
    }
}