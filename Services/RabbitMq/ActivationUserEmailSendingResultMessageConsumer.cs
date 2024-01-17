namespace Services.RabbitMq;

using Common;
using DAL.Abstract;
using Domain;
using Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Services.RabbitMq.Core;

public class ActivationUserEmailSendingResultMessageConsumer(
    RabbitMqSettings settings,
    IContextualSerilogLogger<ActivationUserEmailSendingResultMessageConsumer> contextualLogger,
    IContextualSerilogLogger<RabbitMqConsumerBase<ActivationUserEmailSendingResultMessage,
        ActivationUserEmailSendingResultMessageData>> baseContextualLogger,
    IServiceProvider serviceProvider,
    ICorrelationContextAccessor correlationContextAccessor)
    : RabbitMqConsumerBase<ActivationUserEmailSendingResultMessage, ActivationUserEmailSendingResultMessageData>(
        settings,
        baseContextualLogger,
        MessageQueues.ActivationUserEmailSent,
        correlationContextAccessor)
{
    protected override async Task HandleMessage(ActivationUserEmailSendingResultMessage message)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        var outgoingEmailsRepository = scope.ServiceProvider.GetRequiredService<IOutgoingEmailsRepository>();

        OutgoingEmail outgoingEmail = (await outgoingEmailsRepository.GetById(message.Data.EmailId));

        outgoingEmail.Status = message.Data.SendingStatus;

        await outgoingEmailsRepository.Update(outgoingEmail);
    }
}