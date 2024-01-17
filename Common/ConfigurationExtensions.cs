namespace Common;

using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static TSettings GetSettings<TSettings>(this IConfiguration configuration, string sectionName)
        where TSettings : class, new()
    {
        var settings = new TSettings();

        IConfigurationSection configurationSection = configuration.GetSection(sectionName);
        
        configurationSection.Bind(settings);
        
        return settings;
    }
}