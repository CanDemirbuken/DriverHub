using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Brands.Commands.RemoveBrand;

public sealed record RemoveBrandCommand(Guid Id) : IRequest<Result>;