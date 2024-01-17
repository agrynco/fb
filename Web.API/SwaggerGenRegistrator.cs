namespace Web.API;

// public static class SwaggerGenRegistrar
// {
//     public static void AddSwaggerGen(this IServiceCollection services, string title)
//     {
//         services.AddSwaggerDocument(config =>
//         {
//             config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
//
//             config.AddSecurity("JWT token", new OpenApiSecurityScheme
//             {
//                 Type = OpenApiSecuritySchemeType.ApiKey,
//                 Name = "Authorization",
//                 Description = "Copy 'Bearer ' + valid JWT token into field",
//                 In = OpenApiSecurityApiKeyLocation.Header
//             });
//
//             // config.PostProcess = document =>
//             // {
//             //     document.Info.Version = "v1";
//             //     document.Info.Title = $"{title} - API ({ApplicationEnvironment.GetEnvironment()})";
//             //     document.Info.Description = "ASP.NET Core 8.0 Web-API";
//             // };
//
//             config.GenerateExamples = true;
//             config.UseControllerSummaryAsTagDescription = true;
//             
//             config.UseXmlDocumentation = true;
//         });
//     }
//
//     public static void UseSwagger(this WebApplication app)
//     {
//         app.UseOpenApi();
//
//         app.UseSwaggerUi3(settings =>
//         {
//             settings.DocExpansion = DocExpansion.List.ToString().ToLower();
//             settings.PersistAuthorization = true;
//         });
//     }
// }