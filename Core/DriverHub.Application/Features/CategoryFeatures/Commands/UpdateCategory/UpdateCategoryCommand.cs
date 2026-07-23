using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest;