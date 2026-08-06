namespace DriverHub.WebApi.Common.RateLimiting;

public static class RateLimitPolicyNames
{
    public const string Login = "identity-login";
    public const string Registration = "identity-registration";
    public const string ConfirmEmail = "identity-confirm-email";
    public const string ForgotPassword = "identity-forgot-password";
    public const string ResetPassword = "identity-reset-password";
    public const string RefreshToken = "identity-refresh-token";
}