namespace Services.RabbitMq;

using Common;
using DAL.Abstract;
using Domain;
using Logging;
using Microsoft.Extensions.DependencyInjection;
using Services.RabbitMq.Core;

public class SendPasswordHasBeenChangedMailSendingResultMessageConsumer(
    RabbitMqSettings settings,
    IContextualSerilogLogger<SendPasswordHasBeenChangedMailSendingResultMessageConsumer> contextualLogger,
    IContextualSerilogLogger<RabbitMqConsumerBase<SendPasswordHasBeenChangedMailSendingResultMessage,
        SendPasswordHasBeenChangedMailSendingResultMessageData>> baseContextualLogger,
    IServiceProvider serviceProvider,
    ICorrelationContextAccessor correlationContextAccessor)
    : RabbitMqConsumerBase<SendPasswordHasBeenChangedMailSendingResultMessage, SendPasswordHasBeenChangedMailSendingResultMessageData>(
        settings,
        baseContextualLogger,
        MessageQueues.UserChangedPasswordEmailSent,
        correlationContextAccessor
    )
{
    protected override async Task HandleMessage(SendPasswordHasBeenChangedMailSendingResultMessage message)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        var outgoingEmailsRepository = scope.ServiceProvider.GetRequiredService<IOutgoingEmailsRepository>();

        OutgoingEmail outgoingEmail = (await outgoingEmailsRepository.GetById(message.Data.EmailId));

        outgoingEmail.Status = message.Data.SendingStatus;

        await outgoingEmailsRepository.Update(outgoingEmail);
    }
}