using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<GetCategoryByIdQueryResponse>;