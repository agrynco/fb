using Common;
using Logging;
using Mailing;
using Mailing.Smtp;
using MailSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Services.RabbitMq.Core;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, config) =>
    {
        string? environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

        config.AddCommandLine(args);
    })
    .ConfigureServices((hostingContext, services) =>
    {
        services.AddHostedService<SendUserRegisteredMailMessageConsumer>();
        services.AddHostedService<SendUserForgotPasswordMailMessageConsumer>();
        services.AddHostedService<SendUserChangedPasswordMailMessageConsumer>();
        
        services.AddSingleton(hostingContext.Configuration.GetSettings<RabbitMqSettings>(RabbitMqSettings.SectionName));
        services.AddSingleton(hostingContext.Configuration.GetSettings<SmtpSettings>(SmtpSettings.SectionName));
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        
        services.AddSingleton<ICorrelationContextAccessor, CorrelationContextAccessor>();
        
        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom
            .Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext();
        Log.Logger = loggerConfiguration.CreateLogger();
        
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddProvider(new SerilogLoggerProvider(Log.Logger));
            loggingBuilder.Services.AddSingleton(Log.Logger);
            loggingBuilder.Services.AddTransient(typeof(IContextualSerilogLogger<>),
                typeof(ContextualSerilogLogger<>));
        });
        
        services.AddSingleton<IMailSender, SmtpMailSender>();
    })
    .Build()
    .Run();