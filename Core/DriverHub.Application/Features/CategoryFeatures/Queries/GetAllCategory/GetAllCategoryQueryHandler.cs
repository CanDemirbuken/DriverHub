using DriverHub.Application.Features.CategoryFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Queries.GetAllCategory;

public sealed class GetAllCategoryQueryHandler(IRepository<Category> repository) : IRequestHandler<GetAllCategoryQuery, IReadOnlyList<GetAllCategoryQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllCategoryQueryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await repository.GetAllAsync(cancellationToken);

        IReadOnlyList<GetAllCategoryQueryResponse> response = categories.Select(c => c.ToGetAllResponse()).ToList();
        return response;
    }
}