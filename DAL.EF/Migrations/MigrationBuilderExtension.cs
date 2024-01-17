namespace DAL.EF.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public static class MigrationBuilderExtension
{
    public static void OnlyForIntegrationTests(this MigrationBuilder migrationBuilder,
        Action action)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "IntegrationTests")
        {
            action();
        }
    }
}