namespace Services.RabbitMq;

using Common;
using DAL.Abstract;
using Domain;
using Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Services.RabbitMq.Core;

public class SendUserForgotPasswordMailSendingResultMessageConsumer(
    RabbitMqSettings settings,
    IContextualSerilogLogger<SendUserForgotPasswordMailSendingResultMessageConsumer> contextualLogger,
    IContextualSerilogLogger<RabbitMqConsumerBase<SendUserForgotPasswordMailSendingResultMessage,
        SendUserForgotPasswordMailSendingResultMessageData>> baseContextualLogger,
    IServiceProvider serviceProvider,
    ICorrelationContextAccessor correlationContextAccessor)
    : RabbitMqConsumerBase<SendUserForgotPasswordMailSendingResultMessage,
        SendUserForgotPasswordMailSendingResultMessageData>(
        settings,
        baseContextualLogger,
        MessageQueues.ForgotPasswordEmailSent,
        correlationContextAccessor)
{
    protected override async Task HandleMessage(SendUserForgotPasswordMailSendingResultMessage message)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        var outgoingEmailsRepository = scope.ServiceProvider.GetRequiredService<IOutgoingEmailsRepository>();

        OutgoingEmail outgoingEmail = await outgoingEmailsRepository.GetById(message.Data.EmailId);

        outgoingEmail.Status = message.Data.SendingStatus;

        await outgoingEmailsRepository.Update(outgoingEmail);
    }
}