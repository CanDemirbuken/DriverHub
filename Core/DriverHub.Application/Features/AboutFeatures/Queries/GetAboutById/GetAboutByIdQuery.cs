using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;

public sealed record GetAboutByIdQuery(Guid Id) : IRequest<Result<GetAboutByIdQueryResponse>>;