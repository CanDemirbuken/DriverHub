using FluentValidation;

namespace DriverHub.Application.Common.Validations;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptions<T, Guid> ValidId<T>(this IRuleBuilder<T, Guid> ruleBuilder, string message)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<T, int> ValidPageNumber<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");
    }

    public static IRuleBuilderOptions<T, int> ValidPageSize<T>(this IRuleBuilder<T, int> ruleBuilder, int maximumPageSize)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage("Sayfa boyutu 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(maximumPageSize)
            .WithMessage($"Sayfa boyutu {maximumPageSize} değerini aşamaz.");
    }
}