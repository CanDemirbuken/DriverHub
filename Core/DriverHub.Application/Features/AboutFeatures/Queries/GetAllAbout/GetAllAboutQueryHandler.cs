using DriverHub.Application.Features.AboutFeatures.Mappings;
using DriverHub.Application.Interfaces;
using DriverHub.Domain.Entities;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;

public sealed class GetAllAboutQueryHandler(IRepository<About> repository) : IRequestHandler<GetAllAboutQuery, IReadOnlyList<GetAllAboutQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllAboutQueryResponse>> Handle(GetAllAboutQuery request, CancellationToken cancellationToken)
    {
        var abouts = await repository.GetAllAsync(cancellationToken);

        IReadOnlyList<GetAllAboutQueryResponse> response = abouts.Select(about => about.ToGetAllResponse()).ToList();
        return response;
    }
}