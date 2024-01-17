using Web.API.Core;

namespace Web.API.Controllers.Users.ConfigSections;

public class JwtConfigSection : IConfigSection
{
    public string Key { get; set; } = default!;

    /// <summary>
    ///     In days
    /// </summary>
    public int RefreshTokenTtl { get; set; }

    /// <summary>
    ///     In seconds
    /// </summary>
    public int TokenLiveDuration { get; set; }

    public string SectionName => "Jwt";
}