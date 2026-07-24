using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.CategoryFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories.Abstraction;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(IRepository<Category> repository) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
            throw new NotFoundException();

        GetCategoryByIdQueryResponse response = category.ToGetByIdResponse();
        return response;
    }
}