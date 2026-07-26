using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;

public sealed record UpdateAboutCommand(Guid Id, string Title, string Description, string ImageUrl) : IRequest<Result>;