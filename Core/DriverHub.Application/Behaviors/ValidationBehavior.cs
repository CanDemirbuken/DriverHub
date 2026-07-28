using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DriverHub.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IFailureResult<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        IValidator<TRequest>[] validatorArray = validators.ToArray();

        if (validatorArray.Length == 0)
            return await next(cancellationToken);

        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationResults = await Task.WhenAll(
            validatorArray.Select(validator =>
                validator.ValidateAsync(context, cancellationToken)));

        Error[] errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure =>
                failure is not null &&
                !string.IsNullOrWhiteSpace(failure.ErrorMessage))
            .Select(MapValidationError)
            .Distinct()
            .ToArray();

        if (errors.Length > 0)
            return TResponse.Failure(errors);

        return await next(cancellationToken);
    }

    private static Error MapValidationError(ValidationFailure failure)
    {
        string? field = string.IsNullOrWhiteSpace(failure.PropertyName)
            ? null
            : failure.PropertyName;

        string validatorCode = string.IsNullOrWhiteSpace(failure.ErrorCode)
            ? "Invalid"
            : failure.ErrorCode;

        string code = field is null
            ? $"Validation.{validatorCode}"
            : $"Validation.{field}.{validatorCode}";

        return Error.Validation(
            code,
            failure.ErrorMessage,
            field);
    }
}