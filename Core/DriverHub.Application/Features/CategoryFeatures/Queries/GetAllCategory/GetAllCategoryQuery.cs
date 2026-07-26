using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Queries.GetAllCategory;

public sealed record GetAllCategoryQuery : IRequest<Result<IReadOnlyList<GetAllCategoryQueryResponse>>>;