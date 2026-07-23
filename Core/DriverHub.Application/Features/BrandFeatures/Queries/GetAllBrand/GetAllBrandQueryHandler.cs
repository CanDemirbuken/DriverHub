using DriverHub.Application.Features.BrandFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetAllBrand;

public sealed class GetAllBrandQueryHandler(IRepository<Brand> repository) : IRequestHandler<GetAllBrandQuery, IReadOnlyList<GetAllBrandQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllBrandQueryResponse>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Brand> brands = await repository.GetAllAsync(cancellationToken);

        IReadOnlyList<GetAllBrandQueryResponse> response = brands.Select(b => b.ToGetAllResponse()).ToList();
        return response;
    }
}
