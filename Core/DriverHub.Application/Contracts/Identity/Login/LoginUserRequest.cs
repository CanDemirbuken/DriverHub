namespace DriverHub.Application.Contracts.Identity.Login;

public sealed record LoginUserRequest(string Email, string Password);