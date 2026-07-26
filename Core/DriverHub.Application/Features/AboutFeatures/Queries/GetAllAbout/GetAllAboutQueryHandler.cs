using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;

public sealed class GetAllAboutQueryHandler(IAboutQueryService aboutQueryService) : IRequestHandler<GetAllAboutQuery, Result<IReadOnlyList<GetAllAboutQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllAboutQueryResponse>>> Handle(GetAllAboutQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<GetAllAboutQueryResponse> data = await aboutQueryService.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllAboutQueryResponse>>.Success(data, StatusCodes.Status200OK);
    }
}