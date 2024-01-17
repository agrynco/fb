namespace Mailing.Smtp;

public record SmtpSettings
{
    public static readonly string SectionName = "Smtp";
    public int ClientPort { get; init; }
    public SmtpClientCredentials Credentials { get; set; } = null!;
    public string EmailFrom { get; init; } = null!;
    public bool EnableSsl { get; init; }
    public string Host { get; init; } = null!;
}