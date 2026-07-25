using DriverHub.Application.Exceptions;
using DriverHub.Application.Features.AboutFeatures.Mappings;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;

public sealed class GetAboutByIdQueryHandler(IRepository<About> repository) : IRequestHandler<GetAboutByIdQuery, GetAboutByIdQueryResponse>
{
    public async Task<GetAboutByIdQueryResponse> Handle(GetAboutByIdQuery request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (about is null)
            throw new NotFoundException();

        return about.ToGetByIdResponse();
    }
}