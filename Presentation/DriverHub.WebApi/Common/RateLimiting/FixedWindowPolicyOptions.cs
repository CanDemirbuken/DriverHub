namespace DriverHub.WebApi.Common.RateLimiting;

public sealed record FixedWindowPolicyOptions(int PermitLimit, TimeSpan Window, int QueueLimit = 0);