using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Categories.Commands.RemoveCategory;

public sealed record RemoveCategoryCommand(
    Guid Id
) : IRequest<Result>;