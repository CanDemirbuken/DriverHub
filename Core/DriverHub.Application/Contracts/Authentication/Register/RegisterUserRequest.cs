namespace DriverHub.Application.Contracts.Authentication.Register;

public sealed record RegisterUserRequest(string FirstName, string LastName, string Email, string Password);