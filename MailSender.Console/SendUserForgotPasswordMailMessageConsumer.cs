namespace MailSender;

using Common;
using Domain;
using Logging;
using Mailing;
using Serilog;
using Services.RabbitMq;
using Services.RabbitMq.Core;

public class SendUserForgotPasswordMailMessageConsumer(
    RabbitMqSettings settings,
    IContextualSerilogLogger<SendUserForgotPasswordMailMessageConsumer> contextualLogger,
    IContextualSerilogLogger<RabbitMqConsumerBase<SendUserForgotPasswordMailMessage, SendUserForgotPasswordMailMessageData>>
        baseContextualLogger,
    IMailSender mailSender,
    IRabbitMqService rabbitMqService,
    ICorrelationContextAccessor correlationContextAccessor)
    : RabbitMqConsumerBase<SendUserForgotPasswordMailMessage, SendUserForgotPasswordMailMessageData>(
        settings,
        baseContextualLogger,
        MessageQueues.ForgotPassword,
        correlationContextAccessor)
{
    protected override async Task HandleMessage(SendUserForgotPasswordMailMessage mailMessage)
    {
        var resultMessageData = new SendUserForgotPasswordMailSendingResultMessageData
        {
            EmailId = mailMessage.Data.EmailId,
            UserId = mailMessage.Data.UserId
        };

        if (await mailSender.Send(mailMessage.Data.To, mailMessage.Data.Subject, mailMessage.Data.Body))
        {
            resultMessageData.SendingStatus = EmailSendingStatus.Pending;
        }
        else
        {
            resultMessageData.SendingStatus = EmailSendingStatus.Failed;
        }

        rabbitMqService.SendMessage(MessageQueues.ForgotPasswordEmailSent, resultMessageData);
    }
}