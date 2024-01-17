namespace Web.API;

using Logging;
using Serilog;

public static class SerilogRegistrar
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom
            .Configuration(builder.Configuration)
            .Enrich.FromLogContext();

        Log.Logger = loggerConfiguration.CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger);

        builder.Services.AddTransient(typeof(IContextualSerilogLogger<>),
            typeof(ContextualSerilogLogger<>));
        
        builder.Services.AddSingleton(Log.Logger);
    }
}