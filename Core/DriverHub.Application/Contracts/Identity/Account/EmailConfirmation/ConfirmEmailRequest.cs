namespace DriverHub.Application.Contracts.Identity.Account.EmailConfirmation;

public sealed record ConfirmEmailRequest(string UserId, string ConfirmationToken);