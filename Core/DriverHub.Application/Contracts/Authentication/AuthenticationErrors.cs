using DriverHub.Application.Common.Results;

namespace DriverHub.Application.Contracts.Authentication;

public static class AuthenticationErrors
{
    public static readonly Error EmailAlreadyExists = Error.Conflict(
        "Authentication.EmailAlreadyExists",
        "Bu e-posta adresi daha önce kullanılmıştır.");

    public static readonly Error InvalidCredentials = Error.Unauthorized(
        "Authentication.InvalidCredentials",
        "E-posta veya şifre bilgisi hatalıdır.");

    public static readonly Error AccountLocked = Error.Forbidden(
        "Authentication.AccountLocked",
        "Hesabınız geçici olarak kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");

    public static readonly Error UserInactive = Error.Forbidden(
        "Authentication.UserInactive",
        "Kullanıcı hesabı aktif değildir.");

    public static readonly Error RegistrationFailed = Error.Failure(
        "Authentication.RegistrationFailed",
        "Kullanıcı kaydı gerçekleştirilemedi.");

    public static readonly Error InvalidEmail = Error.Validation(
        "Authentication.InvalidEmail",
        "Geçerli bir e-posta adresi girilmelidir.");

    public static readonly Error InvalidUserName = Error.Validation(
        "Authentication.InvalidUserName",
        "Geçerli bir kullanıcı adı girilmelidir.");

    public static readonly Error PasswordTooShort = Error.Validation(
        "Authentication.PasswordTooShort",
        "Şifre gerekli minimum uzunluğu karşılamamaktadır.");

    public static readonly Error PasswordRequiresDigit = Error.Validation(
        "Authentication.PasswordRequiresDigit",
        "Şifre en az bir rakam içermelidir.");

    public static readonly Error PasswordRequiresLowercase = Error.Validation(
        "Authentication.PasswordRequiresLowercase",
        "Şifre en az bir küçük harf içermelidir.");

    public static readonly Error PasswordRequiresUppercase = Error.Validation(
        "Authentication.PasswordRequiresUppercase",
        "Şifre en az bir büyük harf içermelidir.");

    public static readonly Error PasswordRequiresNonAlphanumeric =
        Error.Validation(
            "Authentication.PasswordRequiresNonAlphanumeric",
            "Şifre en az bir özel karakter içermelidir.");

    public static readonly Error PasswordRequiresUniqueChars =
        Error.Validation(
            "Authentication.PasswordRequiresUniqueChars",
            "Şifre yeterli sayıda farklı karakter içermelidir.");

    public static readonly Error DefaultRoleAssignmentFailed = Error.Failure(
        "Authentication.DefaultRoleAssignmentFailed",
        "Kullanıcıya varsayılan rol atanamadı.");
}