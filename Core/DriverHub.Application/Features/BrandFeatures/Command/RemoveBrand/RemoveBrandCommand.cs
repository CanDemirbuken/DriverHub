using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Command.RemoveBrand;

public sealed record RemoveBrandCommand(Guid Id) : IRequest;