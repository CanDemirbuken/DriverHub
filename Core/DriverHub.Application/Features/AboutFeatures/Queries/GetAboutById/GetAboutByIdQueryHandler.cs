using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;

public sealed class GetAboutByIdQueryHandler(IRepository<About> repository) : IRequestHandler<GetAboutByIdQuery, GetAboutByIdQueryResponse>
{
    public async Task<GetAboutByIdQueryResponse> Handle(GetAboutByIdQuery request, CancellationToken cancellationToken)
    {
        var about = await repository.GetByIdAsync(request.Id, cancellationToken);
        GetAboutByIdQueryResponse response = new GetAboutByIdQueryResponse(
            about.Id,
            about.Title,
            about.Description,
            about.ImageUrl);

        return response;
    }
}