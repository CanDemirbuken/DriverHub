namespace DriverHub.WebApi.Models.Abouts;

public sealed record UpdateAboutRequest(string Title, string Description, string ImageUrl);