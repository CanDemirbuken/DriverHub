using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.BrandFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetBrandById;

public sealed class GetBrandByIdQueryHandler(IRepository<Brand> repository) : IRequestHandler<GetBrandByIdQuery, GetBrandByIdQueryResponse>
{
    public async Task<GetBrandByIdQueryResponse> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
            throw new NotFoundException();

        GetBrandByIdQueryResponse response = brand.ToGetByIdResponse();
        return response;
    }
}