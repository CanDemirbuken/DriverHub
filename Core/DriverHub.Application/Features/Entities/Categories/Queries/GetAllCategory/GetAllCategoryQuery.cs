using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Categories.Queries.GetAllCategory;

public sealed record GetAllCategoryQuery : IRequest<Result<IReadOnlyList<GetAllCategoryQueryResponse>>>;