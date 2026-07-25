namespace DriverHub.Infrastructure.Options;

public sealed class RefreshTokenOptions
{
    public const string SectionName = "Refresh";

    public int ExpireDays { get; set; }
}