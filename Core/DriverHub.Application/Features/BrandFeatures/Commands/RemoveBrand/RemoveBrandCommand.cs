using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Commands.RemoveBrand;

public sealed record RemoveBrandCommand(Guid Id) : IRequest;