using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.RemoveCategory;

public sealed record RemoveCategoryCommand(Guid Id) : IRequest;