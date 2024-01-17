using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace Common;

public static class ConfigurationFactory
{
    /// <summary>
    ///     Use for .NET Core Console applications.
    /// </summary>
    /// <returns></returns>
    public static IConfiguration CreateConfiguration()
    {
        return CreateConfiguration(AppContext.BaseDirectory);
    }

    public static IConfiguration CreateConfiguration(string basePath)
    {
        return CreateConfiguration(basePath, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!);
    }

    public static IConfiguration CreateConfiguration(string basePath, string environmentName)
    {
        var env = new HostingEnvironment
        {
            EnvironmentName = environmentName,
            ApplicationName = AppDomain.CurrentDomain.FriendlyName,
            ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
            ContentRootFileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory)
        };

        var config = new ConfigurationBuilder();
        IConfigurationBuilder configured = Configure(config, env, basePath);
        return configured.Build();
    }

    private static IConfigurationBuilder Configure(IConfigurationBuilder config,
        IHostEnvironment env, string basePath)
    {
        return Configure(config, env.EnvironmentName, basePath);
    }

    private static IConfigurationBuilder Configure(IConfigurationBuilder config, string environmentName,
        string basePath)
    {
        if (!Directory.Exists(basePath))
        {
            throw new DirectoryNotFoundException(
                $"Directory {basePath} does not exist. Wrong value of the {nameof(basePath)} parameter.");
        }

        return config
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables();
    }
}