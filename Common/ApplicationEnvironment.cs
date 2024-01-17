using Microsoft.Extensions.Hosting;

namespace Common;

public static class ApplicationEnvironment
{
    public static bool IsDevelopment => GetEnvironment() == Environments.Development;

    public static string? GetEnvironment()
    {
        return Environment.GetEnvironmentVariable(Constants.ASPNETCORE_ENVIRONMENT);
    }
}