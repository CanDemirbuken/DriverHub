namespace DriverHub.Application.Contracts.Identity.Account.Password;

public sealed record ResetPasswordRequest(string Email, string ResetToken, string NewPassword);