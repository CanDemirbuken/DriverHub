namespace DriverHub.Application.Contracts.Authentication.Login;

public sealed record LoginUserRequest(string Email, string Password);