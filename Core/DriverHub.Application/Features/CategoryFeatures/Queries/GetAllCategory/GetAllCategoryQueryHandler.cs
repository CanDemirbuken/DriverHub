using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.CategoryFeatures.Queries.GetAllCategory;

public sealed class GetAllCategoryQueryHandler(ICategoryQueryService categoryQueryService) : IRequestHandler<GetAllCategoryQuery, Result<IReadOnlyList<GetAllCategoryQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllCategoryQueryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var data = await categoryQueryService.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllCategoryQueryResponse>>.Success(data, StatusCodes.Status200OK);
    }
}