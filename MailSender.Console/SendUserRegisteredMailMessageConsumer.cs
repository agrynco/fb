namespace MailSender;

using Common;
using Domain;
using Logging;
using Mailing;
using Serilog;
using Services.RabbitMq;
using Services.RabbitMq.Core;

public class SendUserRegisteredMailMessageConsumer(
    RabbitMqSettings settings,
    IContextualSerilogLogger<SendUserRegisteredMailMessageConsumer> contextualLogger,
    IContextualSerilogLogger<RabbitMqConsumerBase<SendUserRegisteredMailMessage, SendUserRegisteredMailMessageData>>
        baseContextualLogger,
    IMailSender mailSender,
    IRabbitMqService rabbitMqService,
    ICorrelationContextAccessor correlationContextAccessor)
    : RabbitMqConsumerBase<SendUserRegisteredMailMessage, SendUserRegisteredMailMessageData>(settings,
        baseContextualLogger,
        MessageQueues.UserRegistered,
        correlationContextAccessor)
{
    protected override async Task HandleMessage(SendUserRegisteredMailMessage mailMessage)
    {
        var activationUserEmailSendingResultMessageData = new ActivationUserEmailSendingResultMessageData
        {       EmailId = mailMessage.Data.EmailId,
                UserId = mailMessage.Data.UserId
         };

        if (await mailSender.Send(mailMessage.Data.To, mailMessage.Data.Subject, mailMessage.Data.Body))
        {
            activationUserEmailSendingResultMessageData.SendingStatus = EmailSendingStatus.Sent;
        }
        else
        {
            activationUserEmailSendingResultMessageData.SendingStatus = EmailSendingStatus.Failed;
        }

        rabbitMqService.SendMessage(MessageQueues.ActivationUserEmailSent, activationUserEmailSendingResultMessageData);
    }
}