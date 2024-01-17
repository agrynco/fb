using System.Text;
using System.Text.RegularExpressions;

namespace Services.Users.Mailing;

public static class OutgoingEmailTemplateProcessor
{
    public static string Apply(string template, object model)
    {
        var sb = new StringBuilder(template);
        foreach (string placeholder in GetPlaceholders(template))
        {
            string propertyName = placeholder[2..^2];
            string propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model)?.ToString() ?? string.Empty;
            sb.Replace(placeholder, propertyValue);
        }

        return sb.ToString();
    }
    
    private static string[] GetPlaceholders(string template)
    {
        var matches = Regex.Matches(template, @"{{(.[A-Za-z0-9]+)}}");

        return matches.Select(m => m.Value).Distinct().ToArray();
    }
}