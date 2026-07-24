namespace DriverHub.Application.Common.Results;

public class Result
{
    protected Result(bool isSuccess, IReadOnlyCollection<Error> errors)
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
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public IReadOnlyCollection<Error> Errors { get; }

    public static Result Success()
    {
        return new Result(true, []);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, [error]);
    }

    public static Result Failure(IEnumerable<Error> errors)
    {
        return new Result(false, errors.ToArray());
    }
}

public class Result<T> : Result
{
    private readonly T? _value;

    private Result(T? value, bool isSuccess, IReadOnlyCollection<Error> errors) : base(isSuccess, errors)
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

    public static Result<T> Success(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new Result<T>(
            value,
            true,
            []);
    }

    public static new Result<T> Failure(Error error)
    {
        return new Result<T>(
            default,
            false,
            [error]);
    }

    public static new Result<T> Failure(IEnumerable<Error> errors)
    {
        return new Result<T>(
            default,
            false,
            errors.ToArray());
    }
}