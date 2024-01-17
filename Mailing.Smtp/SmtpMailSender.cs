namespace Mailing.Smtp;

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Serilog;

public class SmtpMailSender(
        SmtpSettings settings,
        ILogger logger)
    : IMailSender
{
    public async Task<bool> Send(string to, string subject, string body)
    {
        try
        {
            var mail = new MailMessage
            {
                Subject = subject,
                To =
                {
                    to
                },
                From = new MailAddress(settings.EmailFrom),
                Body = body,
                IsBodyHtml = true
            };

            using var smtpClient = new SmtpClient(settings.Host, settings.ClientPort);

            smtpClient.EnableSsl = settings.EnableSsl;
            smtpClient.Credentials = new NetworkCredential(settings.Credentials.Username, settings.Credentials.Password);
                
            await smtpClient.SendMailAsync(mail);

            return true;
        }
        catch (Exception e)
        {
            logger.Error(e, "{Handler}: Error => {@Error}", GetType(), e.Message);
        }

        return false;
    }
}