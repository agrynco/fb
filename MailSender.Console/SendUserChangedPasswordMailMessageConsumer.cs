namespace MailSender;

using Common;
using Domain;
using Logging;
using Mailing;
using Serilog;
using Services.RabbitMq;
using Services.RabbitMq.Core;

public class SendUserChangedPasswordMailMessageConsumer(
    RabbitMqSettings settings,
    IMailSender mailSender,
    IContextualSerilogLogger<SendUserChangedPasswordMailMessageConsumer> contextualLogger,
    IContextualSerilogLogger<
            RabbitMqConsumerBase<SendUserChangedPasswordMailMessage, SendUserChangedPasswordMailMessageData>>
        baseContextualLogger,
    IRabbitMqService rabbitMqService,
    ICorrelationContextAccessor correlationContextAccessor)
    : RabbitMqConsumerBase<SendUserChangedPasswordMailMessage, SendUserChangedPasswordMailMessageData>(
        settings,
        baseContextualLogger,
        MessageQueues.UserChangedPasswordEmailSent, correlationContextAccessor)
{
    protected override async Task HandleMessage(SendUserChangedPasswordMailMessage mailMessage)
    {
        var sendingResultMessage = new SendPasswordHasBeenChangedMailSendingResultMessage()
        {
            Data = new SendPasswordHasBeenChangedMailSendingResultMessageData
            {
                EmailId = mailMessage.Data.EmailId,
                UserId = mailMessage.Data.UserId
            }
        };

        if (await mailSender.Send(mailMessage.Data.To, mailMessage.Data.Subject, mailMessage.Data.Body))
        {
            sendingResultMessage.Data.SendingStatus = EmailSendingStatus.Sent;
        }
        else
        {
            sendingResultMessage.Data.SendingStatus = EmailSendingStatus.Failed;
        }

        rabbitMqService.SendMessage(MessageQueues.UserChangedPasswordEmailSent, sendingResultMessage);
    }
}