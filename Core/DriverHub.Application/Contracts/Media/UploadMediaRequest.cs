namespace DriverHub.Application.Contracts.Media;

public sealed record UploadMediaRequest(string FileName, string ContentType, long Length, Stream Content);