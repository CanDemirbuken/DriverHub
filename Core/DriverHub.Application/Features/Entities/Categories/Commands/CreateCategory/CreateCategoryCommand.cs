using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.CategoryFeatures.Commands.CreateCategory;
using MediatR;

namespace DriverHub.Application.Features.Entities.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name
) : IRequest<Result<CreateCategoryCommandResponse>>;