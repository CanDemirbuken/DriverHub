namespace DriverHub.Application.Common.Errors.Identity;

public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentials = new(
        "Authentication.InvalidCredentials",
        "E-mail ya da şifre hatalı.",
        ErrorType.Unauthorized);

    public static readonly Error UserLocked = new(
        "Authentication.UserLocked",
        "Kullanıcı hesabı başarısız giriş denemeleri nedeniyle geçici olarak kilitlenmiştir.",
        ErrorType.Locked);

    public static readonly Error EmailNotConfirmed = new(
        "Authentication.EmailNotConfirmed",
        "E-mail adresi onaylanmamış.",
        ErrorType.Forbidden);

    public static readonly Error InvalidUser = new(
        "Authentication.InvalidUser",
        "Kullanıcı bilgisi geçersiz.",
        ErrorType.Unauthorized);
}