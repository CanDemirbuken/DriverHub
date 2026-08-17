using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Media;
using DriverHub.Application.Interfaces.Media;
using MediatR;

namespace DriverHub.Application.Features.Media.Commands;

public sealed class UploadMediaCommandHandler(IStorageService storageService) : IRequestHandler<UploadMediaCommand, Result<UploadMediaCommandResponse>>
{
    public async Task<Result<UploadMediaCommandResponse>> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
    {
        UploadMediaRequest uploadMediaRequest = new(
            request.FileName,
            request.ContentType,
            request.Length,
            request.Content
        );

        Result<UploadMediaResponse> storageResult =
            await storageService.UploadAsync(
                uploadMediaRequest,
                cancellationToken
            );

        if (!storageResult.IsSuccess)
            return Result<UploadMediaCommandResponse>.Failure(storageResult.Errors);

        UploadMediaCommandResponse response = new(storageResult.Value.Path);
        return Result<UploadMediaCommandResponse>.Success(response);
    }
}