using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.FeatureFeatures.Commands.RemoveFeature;

public sealed record RemoveFeatureCommand(Guid Id) : IRequest<Result>;