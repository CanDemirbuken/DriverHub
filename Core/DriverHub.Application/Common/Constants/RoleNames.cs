namespace DriverHub.Application.Common.Constants;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string User = "User";

    public static readonly IReadOnlyCollection<string> All =
    [
        Admin,
        User
    ];
}