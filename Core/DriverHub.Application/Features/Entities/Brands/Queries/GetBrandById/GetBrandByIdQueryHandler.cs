using AutoMapper;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Brands.Queries.GetBrandById;

public sealed class GetBrandByIdQueryHandler(IRepository<Brand> repository, IMapper mapper) : IRequestHandler<GetBrandByIdQuery, Result<GetBrandByIdQueryResponse>>
{
    public async Task<Result<GetBrandByIdQueryResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
            return Result<GetBrandByIdQueryResponse>.Failure(Error.NotFound($"{request.Id} kimliğine sahip kayıt bulunamadı.", nameof(request.Id)));

        GetBrandByIdQueryResponse response = mapper.Map<GetBrandByIdQueryResponse>(brand);
        return Result<GetBrandByIdQueryResponse>.Success(response);
    }
}