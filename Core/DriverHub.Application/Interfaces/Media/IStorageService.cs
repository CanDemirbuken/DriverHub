using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Media;

namespace DriverHub.Application.Interfaces.Media;

public interface IStorageService
{
    Task<Result<UploadMediaResponse>> UploadAsync(UploadMediaRequest request, CancellationToken cancellationToken = default);
}
