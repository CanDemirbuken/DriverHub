namespace DriverHub.WebApi.Common.RateLimiting;

public static class RateLimitPolicies
{
    public static readonly FixedWindowPolicyOptions Login = new(
        PermitLimit: 5,
        Window: TimeSpan.FromMinutes(1));

    public static readonly FixedWindowPolicyOptions Registration = new(
        PermitLimit: 3,
        Window: TimeSpan.FromMinutes(10));

    public static readonly FixedWindowPolicyOptions ConfirmEmail = new(
        PermitLimit: 3,
        Window: TimeSpan.FromMinutes(15));

    public static readonly FixedWindowPolicyOptions ForgotPassword = new(
        PermitLimit: 3,
        Window: TimeSpan.FromMinutes(15));

    public static readonly FixedWindowPolicyOptions ResetPassword = new(
        PermitLimit: 5,
        Window: TimeSpan.FromMinutes(15));

    public static readonly FixedWindowPolicyOptions RefreshToken = new(
        PermitLimit: 10,
        Window: TimeSpan.FromMinutes(1));
}