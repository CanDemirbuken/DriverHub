using DriverHub.Application.Features.BannerFeatures.Queries.GetAllBanner;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

using System.Text.Json.Serialization;

namespace DriverHub.Application.Common.Results;

public class Result
{
    protected Result(bool isSuccess, int statusCode, IReadOnlyCollection<string> errors)
    {
        if (isSuccess && errors.Count > 0)
        {
            throw new InvalidOperationException(
                "Başarılı bir sonuç hata içeremez.");
        }

        if (!isSuccess && errors.Count == 0)
        {
            throw new InvalidOperationException(
                "Başarısız bir sonuç en az bir hata içermelidir.");
        }

        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    [JsonIgnore] public int StatusCode { get; }
    public IReadOnlyCollection<string> Errors { get; }

    public static Result Success(int statusCode)
    {
        return new Result(
            true,
            statusCode,
            []);
    }

    public static Result Failure(int statusCode, string error)
    {
        return new Result(
            false,
            statusCode,
            [error]);
    }

    public static Result Failure(int statusCode, IEnumerable<string> errors)
    {
        return new Result(
            false,
            statusCode,
            errors.ToArray());
    }
}

public class Result<T> : Result
{
    private readonly T? _value;

    private Result(T? value, bool isSuccess, int statusCode, IReadOnlyCollection<string> errors) : base(isSuccess, statusCode, errors)
    {
        _value = value;
    }

    public T Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidOperationException(
                    "Başarısız bir sonuçtan değer okunamaz.");
            }

            return _value!;
        }
    }

    public static Result<T> Success(T value, int statusCode)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new Result<T>(
            value,
            true,
            statusCode,
            []);
    }

    public static Result<T> Failure(int statusCode, string error)
    {
        return new Result<T>(
            default,
            false,
            statusCode,
            [error]);
    }

    public static Result<T> Failure(int statusCode, IEnumerable<string> errors)
    {
        return new Result<T>(
            default,
            false,
            statusCode,
            errors.ToArray());
    }

    internal static Result<IReadOnlyList<GetAllBannerQueryResponse>> Success(object statusCode)
    {
        throw new NotImplementedException();
    }
}