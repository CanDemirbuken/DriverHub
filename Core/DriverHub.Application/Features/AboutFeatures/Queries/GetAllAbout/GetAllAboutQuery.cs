using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;

public sealed record GetAllAboutQuery : IRequest<Result<IReadOnlyList<GetAllAboutQueryResponse>>>;