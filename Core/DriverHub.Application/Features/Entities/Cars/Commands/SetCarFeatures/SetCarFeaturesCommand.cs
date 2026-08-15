using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.SetCarFeatures;

public sealed record SetCarFeaturesCommand(
    Guid Id,
    IReadOnlyCollection<Guid> FeatureIds
) : IRequest<Result>;