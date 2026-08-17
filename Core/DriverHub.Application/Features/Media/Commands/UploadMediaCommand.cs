using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Media.Commands;

public sealed record UploadMediaCommand(string FileName, string ContentType, long Length, Stream Content) : IRequest<Result<UploadMediaCommandResponse>>;