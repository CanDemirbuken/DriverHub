using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Queries.GetAllCategory;

public sealed record GetAllCategoryQuery : IRequest<IReadOnlyList<GetAllCategoryQueryResponse>>;