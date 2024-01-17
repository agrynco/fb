namespace Web.API.IntegrationTests.Core;

using DAL.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Serilog;
using Services.RabbitMq;
using Services.RabbitMq.Core;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = Guid.NewGuid().ToString();

    public override ValueTask DisposeAsync()
    {
        Log.CloseAndFlush();

        return base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");
        builder.ConfigureServices(SubstituteApplicationDbContext);
        builder.ConfigureServices(SubstituteRabbitMqService);
    }
    
    private void SubstituteRabbitMqService(IServiceCollection services)
    {
        
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IRabbitMqService));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
        
        Mock<IRabbitMqService> mock = new Mock<IRabbitMqService>();
        mock.Setup(service => service.SendMessage(It.IsAny<MessageQueues>(), It.IsAny<object>()))
            .Verifiable();

        services.AddTransient(_ => mock.Object);
    }

    private void SubstituteApplicationDbContext(IServiceCollection services)
    {
        ServiceDescriptor? descriptor =
            services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase(_dbName);
            options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

        ServiceProvider sp = services.BuildServiceProvider();

        using IServiceScope scope = sp.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            appContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Message: {Message}", ex.Message);
            throw;
        }
    }
}