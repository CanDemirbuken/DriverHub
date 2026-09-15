using DriverHub.WebApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace DriverHub.Tests;

public sealed class AppDbContextFactoryTests
{
    [Fact]
    public void Command_line_connection_overrides_configuration_without_connecting_to_database()
    {
        const string connection = "Server=localhost;Database=DesignTimeConfigurationTest;Integrated Security=true";
        using var context = new AppDbContextFactory().CreateDbContext(
            ["--environment", "Testing", "--SqlOptions:ConnectionString", connection]);
        var resolved = new SqlConnectionStringBuilder(context.Database.GetConnectionString());
        Assert.Equal("localhost", resolved.DataSource);
        Assert.Equal("DesignTimeConfigurationTest", resolved.InitialCatalog);
        Assert.True(resolved.IntegratedSecurity);
    }

    [Fact]
    public void Empty_connection_produces_actionable_error_before_opening_database()
    {
        var exception = Assert.Throws<InvalidOperationException>(() => new AppDbContextFactory().CreateDbContext(
            ["--environment", "Testing", "--SqlOptions:ConnectionString", ""]));
        Assert.Contains("SqlOptions:ConnectionString", exception.Message);
        Assert.Contains("User Secrets", exception.Message);
    }
}
