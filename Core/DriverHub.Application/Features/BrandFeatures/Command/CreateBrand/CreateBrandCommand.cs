using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Command.CreateBrand;

public sealed record CreateBrandCommand(string Name) : IRequest;