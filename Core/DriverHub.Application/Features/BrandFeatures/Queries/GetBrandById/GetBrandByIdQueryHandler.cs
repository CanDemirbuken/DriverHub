using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.BrandFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.BrandFeatures.Queries.GetBrandById;

public sealed class GetBrandByIdQueryHandler(IRepository<Brand> repository) : IRequestHandler<GetBrandByIdQuery, Result<GetBrandByIdQueryResponse>>
{
    public async Task<Result<GetBrandByIdQueryResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
            return Result<GetBrandByIdQueryResponse>.Failure(StatusCodes.Status404NotFound, $"{request.Id} kimliğine sahip kayıt bulunamadı.");

        GetBrandByIdQueryResponse response = brand.ToGetByIdResponse();
        return Result<GetBrandByIdQueryResponse>.Success(response, StatusCodes.Status200OK);
    }
}