namespace DriverHub.Application.Contracts.Identity.Register;

public sealed record RegisterUserRequest(string FirstName, string LastName, string Email, string Password);