using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Media;
using DriverHub.Application.Interfaces.Media;
using Microsoft.AspNetCore.Hosting;

namespace DriverHub.Infrastructure.Services.Media;

public sealed class StorageService(IWebHostEnvironment env) : IStorageService
{
    private readonly IWebHostEnvironment _env = env;

    private const long MaxFileSize = 5 * 1024 * 1024;

    private static readonly HashSet<string> AcceptedExtensions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

    private static readonly HashSet<string> AcceptedContentTypes =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg",
            "image/png",
            "image/webp"
        };

    public async Task<Result<UploadMediaResponse>> UploadAsync(
        UploadMediaRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request.Length <= 0)
        {
            return Result<UploadMediaResponse>.Failure(
                Error.Validation(
                    "Media.EmptyFile",
                    "Dosya boş olamaz."
                )
            );
        }

        if (request.Length > MaxFileSize)
        {
            return Result<UploadMediaResponse>.Failure(
                Error.Validation(
                    "Media.FileTooLarge",
                    "Dosya boyutu en fazla 5 MB olabilir."
                )
            );
        }

        string extension = Path.GetExtension(request.FileName);

        if (string.IsNullOrWhiteSpace(extension))
        {
            return Result<UploadMediaResponse>.Failure(
                Error.Validation(
                    "Media.InvalidExtension",
                    "Dosya uzantısı bulunamadı."
                )
            );
        }

        extension = extension.ToLowerInvariant();

        if (!AcceptedExtensions.Contains(extension))
        {
            return Result<UploadMediaResponse>.Failure(
                Error.Validation(
                    "Media.UnsupportedExtension",
                    "Dosya uzantısı desteklenmiyor."
                )
            );
        }

        if (string.IsNullOrWhiteSpace(request.ContentType))
        {
            return Result<UploadMediaResponse>.Failure(
                Error.Validation(
                    "Media.InvalidContentType",
                    "Dosya içerik tipi bulunamadı."
                )
            );
        }

        if (!AcceptedContentTypes.Contains(request.ContentType))
        {
            return Result<UploadMediaResponse>.Failure(
                Error.Validation(
                    "Media.UnsupportedContentType",
                    "Dosya içerik tipi desteklenmiyor."
                )
            );
        }

        await using MemoryStream bufferedContent = new();

        await request.Content.CopyToAsync(
            bufferedContent,
            cancellationToken
        );

        if (bufferedContent.Length <= 0)
        {
            return Result<UploadMediaResponse>.Failure(
                Error.Validation(
                    "Media.EmptyFile",
                    "Dosya içeriği boş olamaz."
                )
            );
        }

        if (bufferedContent.Length > MaxFileSize)
        {
            return Result<UploadMediaResponse>.Failure(
                Error.Validation(
                    "Media.FileTooLarge",
                    "Dosya boyutu en fazla 5 MB olabilir."
                )
            );
        }

        bufferedContent.Position = 0;

        Error? signatureError =
            await ValidateFileSignatureAsync(
                bufferedContent,
                extension,
                cancellationToken
            );

        if (signatureError is not null)
        {
            return Result<UploadMediaResponse>.Failure(
                signatureError
            );
        }

        bufferedContent.Position = 0;

        string fileName =
            $"{Guid.NewGuid():N}{extension}";

        string webRootPath =
            _env.WebRootPath;

        if (string.IsNullOrWhiteSpace(webRootPath))
        {
            webRootPath = Path.Combine(
                _env.ContentRootPath,
                "wwwroot"
            );
        }

        string storageDirectory = Path.Combine(
            webRootPath,
            "uploads",
            "images"
        );

        Directory.CreateDirectory(
            storageDirectory
        );

        string physicalPath = Path.Combine(
            storageDirectory,
            fileName
        );

        await using FileStream destinationStream = new(
            physicalPath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None
        );

        await bufferedContent.CopyToAsync(
            destinationStream,
            cancellationToken
        );

        string publicPath =
            $"/uploads/images/{fileName}";

        UploadMediaResponse response = new(
            publicPath
        );

        return Result<UploadMediaResponse>.Success(
            response
        );
    }

    private static async Task<Error?> ValidateFileSignatureAsync(
        Stream content,
        string extension,
        CancellationToken cancellationToken)
    {
        const int HeaderLength = 12;

        byte[] header = new byte[HeaderLength];

        int bytesRead = await content.ReadAsync(
            header.AsMemory(0, HeaderLength),
            cancellationToken
        );

        bool isValid = extension switch
        {
            ".jpg" or ".jpeg" =>
                IsJpeg(header, bytesRead),

            ".png" =>
                IsPng(header, bytesRead),

            ".webp" =>
                IsWebP(header, bytesRead),

            _ =>
                false
        };

        if (!isValid)
        {
            return Error.Validation(
                "Media.InvalidFileSignature",
                "Dosyanın gerçek içeriği dosya uzantısıyla eşleşmiyor."
            );
        }

        return null;
    }

    private static bool IsJpeg(
        byte[] header,
        int bytesRead)
    {
        return
            bytesRead >= 3 &&
            header[0] == 0xFF &&
            header[1] == 0xD8 &&
            header[2] == 0xFF;
    }

    private static bool IsPng(
        byte[] header,
        int bytesRead)
    {
        if (bytesRead < 8)
        {
            return false;
        }

        return
            header[0] == 0x89 &&
            header[1] == 0x50 &&
            header[2] == 0x4E &&
            header[3] == 0x47 &&
            header[4] == 0x0D &&
            header[5] == 0x0A &&
            header[6] == 0x1A &&
            header[7] == 0x0A;
    }

    private static bool IsWebP(
        byte[] header,
        int bytesRead)
    {
        if (bytesRead < 12)
        {
            return false;
        }

        bool hasRiffSignature =
            header[0] == (byte)'R' &&
            header[1] == (byte)'I' &&
            header[2] == (byte)'F' &&
            header[3] == (byte)'F';

        bool hasWebPSignature =
            header[8] == (byte)'W' &&
            header[9] == (byte)'E' &&
            header[10] == (byte)'B' &&
            header[11] == (byte)'P';

        return
            hasRiffSignature &&
            hasWebPSignature;
    }
}