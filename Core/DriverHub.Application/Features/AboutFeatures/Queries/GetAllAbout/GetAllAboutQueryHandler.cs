using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;

public sealed class GetAllAboutQueryHandler(IAboutQueryService aboutQueryService) : IRequestHandler<GetAllAboutQuery, IReadOnlyList<GetAllAboutQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllAboutQueryResponse>> Handle(GetAllAboutQuery request, CancellationToken cancellationToken)
    {
        return await aboutQueryService.GetAllAsync(cancellationToken);
    }
}