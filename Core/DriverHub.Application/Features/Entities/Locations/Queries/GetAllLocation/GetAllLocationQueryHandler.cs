using AutoMapper;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Queries.GetAllLocation;

public sealed class GetAllLocationQueryHandler(IRepository<Location> repository, IMapper mapper) : IRequestHandler<GetAllLocationQuery, Result<IReadOnlyList<GetAllLocationQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllLocationQueryResponse>>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Location> locations = await repository.GetAllAsync(cancellationToken);

        IReadOnlyList<GetAllLocationQueryResponse> data = mapper.Map<IReadOnlyList<GetAllLocationQueryResponse>>(locations);

        return Result<IReadOnlyList<GetAllLocationQueryResponse>>.Success(data);
    }
}