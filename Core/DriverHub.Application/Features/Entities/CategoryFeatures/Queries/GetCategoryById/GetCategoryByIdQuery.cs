using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.CategoryFeatures.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<Result<GetCategoryByIdQueryResponse>>;