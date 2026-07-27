using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.FeatureFeatures.Commands.CreateFeature;

public sealed record CreateFeatureCommand(string Name) : IRequest<Result<CreateFeatureCommandResponse>>;