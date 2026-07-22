using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Command.UpdateBrand;

public sealed record UpdateBrandCommand(Guid Id, string Name) : IRequest;