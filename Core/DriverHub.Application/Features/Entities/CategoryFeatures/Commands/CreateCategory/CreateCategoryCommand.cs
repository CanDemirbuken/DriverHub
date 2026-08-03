using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.CategoryFeatures.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : IRequest<Result<CreateCategoryCommandResponse>>;