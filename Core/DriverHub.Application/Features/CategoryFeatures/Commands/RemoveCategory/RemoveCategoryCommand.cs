using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.CategoryFeatures.Commands.RemoveCategory;

public sealed record RemoveCategoryCommand(Guid Id) : IRequest<Result>;