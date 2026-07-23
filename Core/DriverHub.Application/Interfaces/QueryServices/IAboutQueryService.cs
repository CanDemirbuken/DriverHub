using DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IAboutQueryService
{
    Task<IReadOnlyList<GetAllAboutQueryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}