using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Commands.UpdateBrand;

public sealed record UpdateBrandCommand(Guid Id, string Name) : IRequest<Result>;