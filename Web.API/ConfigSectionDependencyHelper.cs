using Web.API.Core;

namespace Web.API;

public static class ConfigSectionDependencyHelper
{
    public static void Register<TConfigSection>(this WebApplicationBuilder webApplicationBuilder)
        where TConfigSection : class, IConfigSection, new()
    {
        var configSection = new TConfigSection();
        webApplicationBuilder.Configuration.GetRequiredSection(configSection.SectionName).Bind(configSection);
        webApplicationBuilder.Services.AddSingleton(configSection);
    }
}