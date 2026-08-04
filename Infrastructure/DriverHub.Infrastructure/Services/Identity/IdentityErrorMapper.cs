using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Identity;

internal static class IdentityErrorMapper
{
    public static IReadOnlyCollection<string> Map(IEnumerable<IdentityError> identityErrors)
    {
        ArgumentNullException.ThrowIfNull(identityErrors);

        return identityErrors
            .Select(Map)
            .Distinct()
            .ToArray();
    }

    private static string Map(IdentityError identityError)
    {
        return identityError.Code switch
        {
            nameof(IdentityErrorDescriber.DuplicateEmail)
                => "Bu e-posta adresi daha önce kullanılmıştır.",

            nameof(IdentityErrorDescriber.DuplicateUserName)
                => "Bu e-posta adresi daha önce kullanılmıştır.",

            nameof(IdentityErrorDescriber.InvalidEmail)
                => "Geçerli bir e-posta adresi girilmelidir.",

            nameof(IdentityErrorDescriber.InvalidUserName)
                => "Geçerli bir kullanıcı adı girilmelidir.",

            nameof(IdentityErrorDescriber.PasswordTooShort)
                => "Şifre gerekli minimum uzunluğu karşılamamaktadır.",

            nameof(IdentityErrorDescriber.PasswordRequiresDigit)
                => "Şifre en az bir rakam içermelidir.",

            nameof(IdentityErrorDescriber.PasswordRequiresLower)
                => "Şifre en az bir küçük harf içermelidir.",

            nameof(IdentityErrorDescriber.PasswordRequiresUpper)
                => "Şifre en az bir büyük harf içermelidir.",

            nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric)
                => "Şifre en az bir özel karakter içermelidir.",

            nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars)
                => "Şifre yeterli sayıda farklı karakter içermelidir.",

            nameof(IdentityErrorDescriber.DuplicateRoleName)
                => "Bu rol adı daha önce kullanılmıştır.",

            nameof(IdentityErrorDescriber.InvalidRoleName)
                => "Geçerli bir rol adı girilmelidir.",

            nameof(IdentityErrorDescriber.UserAlreadyInRole)
                => "Kullanıcı ilgili role zaten sahiptir.",

            nameof(IdentityErrorDescriber.UserNotInRole)
                => "Kullanıcı ilgili role sahip değildir.",

            nameof(IdentityErrorDescriber.ConcurrencyFailure)
                => "Kayıt başka bir işlem tarafından değiştirilmiştir. Lütfen tekrar deneyiniz.",

            _ => "Kimlik yönetimi işlemi gerçekleştirilemedi."
        };
    }
}