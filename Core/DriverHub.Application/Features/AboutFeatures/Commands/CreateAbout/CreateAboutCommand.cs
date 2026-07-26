using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Commands.CreateAbout;

public sealed record CreateAboutCommand(string Title, string Description, string ImageUrl) : IRequest<Result<CreateAboutCommandResponse>>;