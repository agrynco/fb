namespace Mailing.Smtp;

public record SmtpClientCredentials
{
    public string Username { get; set; }
    public string Password { get; set; }
}