namespace DriverHub.Application.Contracts.Identity.Account.Register;

public sealed record RegisterUserRequest(string FirstName, string LastName, string Email, string Password);