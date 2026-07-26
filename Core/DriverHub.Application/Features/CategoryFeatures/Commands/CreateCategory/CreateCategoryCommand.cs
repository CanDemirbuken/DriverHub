using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : IRequest<Result<CreateCategoryCommandResponse>>;