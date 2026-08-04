namespace DriverHub.Application.Common.Errors.Identity;

public static class SessionErrors
{
    public static readonly Error InvalidRefreshToken = new(
        "Session.InvalidRefreshToken",
        "Refresh token geçersiz veya kullanım süresi dolmuş.",
        ErrorType.Unauthorized,
        "RefreshToken");

    public static readonly Error ReusedRefreshToken = new(
        "Session.ReusedRefreshToken",
        "Refresh token daha önce kullanılmış veya geçersiz hale getirilmiştir.",
        ErrorType.Unauthorized,
        "RefreshToken");
}