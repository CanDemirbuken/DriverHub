namespace DriverHub.Persistence.Options.Sql;

public sealed class SqlOptions
{
    public const string SectionName = "SqlOptions";

    public string ConnectionString { get; set; } = string.Empty;
}