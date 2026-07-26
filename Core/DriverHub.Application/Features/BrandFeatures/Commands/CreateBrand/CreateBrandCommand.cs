using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Commands.CreateBrand;

public sealed record CreateBrandCommand(string Name) : IRequest<Result<CreateBrandCommandResponse>>;