namespace DAL.EF.Migrator;

using System.Reflection;
using Common;
using Common.Console;
using DAL.EF.Core;
using DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DbMigrator
{
    public delegate void BeforeMigrateDelegate(string pathToConfigs, string environmentName,
        string connectionString);

    public static void Migrate(string basePath, string environmentName, bool cleanDb, bool fillData)
    {
        IConfiguration configuration = ConfigurationFactory.CreateConfiguration(basePath, environmentName);
        ServiceProvider serviceProvider = BuildServiceProvider(configuration, environmentName);

        MigrateMainDb(basePath, environmentName, cleanDb, fillData, serviceProvider, configuration);
    }

    public static void MigrateMainDb(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        MigrateMainDb(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, ApplicationEnvironment.GetEnvironment()!, false, false,
            serviceProvider, configuration);
    }
    
    private static void MigrateMainDb(string basePath, string environmentName, bool cleanDb, bool fillData,
        IServiceProvider serviceProvider, IConfiguration configuration)
    {
        OnBeforeMigrate(basePath, environmentName, configuration);
        using IServiceScope scope = serviceProvider.CreateScope();

        if (cleanDb)
        {
            throw new NotSupportedException($"{nameof(cleanDb)} = {true}");
        }

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        string[] pendingMigrations = dbContext.Database.GetPendingMigrations().ToArray();

        if (pendingMigrations.Any())
        {
            foreach (string migration in pendingMigrations)
            {
                ConsoleExtensions.WriteWarning($"Applying migration {migration}".PadRight(100, '.'), false);
                dbContext.Database.GetInfrastructure().GetRequiredService<IMigrator>().Migrate(migration);
                ConsoleExtensions.WriteInfo("Done");
            }
        }
        else
        {
            ConsoleExtensions.WriteInfo("There is no pending migrations.");
        }

        if (fillData)
        {
            throw new NotSupportedException($"{nameof(fillData)} = {true}");
        }
    }

    private static ServiceProvider BuildServiceProvider(IConfiguration configuration, string environmentName)
    {
        var serviceCollection = new ServiceCollection();
        DependencyHelper.RegisterServices(serviceCollection, configuration, environmentName);

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider;
    }

    private static void OnBeforeMigrate(string basePath, string environmentName, IConfiguration configuration)
    {
        if (BeforeMigrate == null)
        {
            return;
        }

        const string PASSWORD = "********";

        string connectionString = new SqlConnectionStringBuilder(configuration, "Default")
        {
            Password = PASSWORD
        }.ToString();

        BeforeMigrate(basePath, environmentName, connectionString);
    }

    public static event BeforeMigrateDelegate? BeforeMigrate;
}