using Common.Console;
using DAL.EF.Migrator;
using DI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Services.Users.Authenticate;
using Web.API.Controllers;
using Web.API.Controllers.Users.ConfigSections;

namespace Web.API;

using System.Reflection;
using Common;
using Microsoft.OpenApi.Models;

public class Program
{
    protected Program()
    {
    }

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpContextAccessor();

        builder.Logging.ClearProviders();
        builder.AddSerilog();

        builder.Services.AddMvc().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });

        builder.Services.AddSingleton<ICorrelationContextAccessor, HttpContextCorrelationContextAccessor>();
        DependencyHelper.RegisterServices(builder.Services, builder.Configuration, builder.Environment.EnvironmentName);

        builder.Register<JwtConfigSection>();

        builder.AddSlimMessageBus(new[]
            {
                typeof(SystemController).Assembly, typeof(AuthenticateUserByCredentialsRequest).Assembly
            },
            new[]
            {
                typeof(SystemController).Assembly, typeof(AuthenticateUserByCredentialsRequest).Assembly
            });

        builder.Services.AddControllers().AddNewtonsoftJson();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        builder.RegisterAuthentication();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });

            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        WebApplication app = builder.Build();

        app.UseCors(corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyHeader();
            corsPolicyBuilder.AllowAnyMethod();
            corsPolicyBuilder.AllowAnyOrigin();
            corsPolicyBuilder.WithExposedHeaders("Content-Disposition");
        });

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        if (builder.Configuration.GetValue<bool>("EnableDbMigrations"))
        {
            ConsoleExtensions.WriteInfo("EnableDbMigrations = true");
            DbMigrator.MigrateMainDb(app.Services, app.Configuration);
        }
        else
        {
            ConsoleExtensions.WriteInfo("EnableDbMigrations = false");
        }

        app.Run();
    }
}