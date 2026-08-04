namespace DriverHub.Application.Contracts.Identity.Authentication.Login;

public sealed record LoginUserRequest(string Email, string Password);