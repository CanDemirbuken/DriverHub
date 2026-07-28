namespace DriverHub.Application.Common.Errors;

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

    public static readonly Error EmailAlreadyExists = new(
        "Authentication.EmailAlreadyExists",
        "Bu e-mail adresi ile kayıtlı bir kullanıcı bulunmaktadır.",
        ErrorType.Conflict,
        "Email");

    public static readonly Error DefaultRoleAssignmentFailed = new(
        "Authentication.DefaultRoleAssignmentFailed",
        "Kullanıcıya varsayılan rol atanamadı.",
        ErrorType.Failure);

    public static readonly Error InvalidRefreshToken = new(
        "Authentication.InvalidRefreshToken",
        "Refresh token geçersiz veya kullanım süresi dolmuş.",
        ErrorType.Unauthorized,
        "RefreshToken");

    public static readonly Error ReusedRefreshToken = new(
        "Authentication.ReusedRefreshToken",
        "Refresh token daha önce kullanılmış veya geçersiz hale getirilmiştir.",
        ErrorType.Unauthorized,
        "RefreshToken");

    public static readonly Error InvalidUser = new(
        "Authentication.InvalidUser",
        "Kullanıcı bilgisi geçersiz.",
        ErrorType.Unauthorized);
}