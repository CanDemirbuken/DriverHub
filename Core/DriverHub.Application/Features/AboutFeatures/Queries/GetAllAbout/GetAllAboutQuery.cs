using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;

public sealed record GetAllAboutQuery : IRequest<IReadOnlyList<GetAllAboutQueryResponse>>;