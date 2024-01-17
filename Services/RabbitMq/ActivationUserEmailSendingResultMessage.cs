namespace Services.RabbitMq;

using Common;
using Domain;
using Services.RabbitMq.Core;

public record ActivationUserEmailSendingResultMessageData : ISendMessageFromMailSender
{
    public int EmailId { get; set; }
    public int UserId { get; set; }
    public EmailSendingStatus SendingStatus { get; set; }
}

public record ActivationUserEmailSendingResultMessage : IRabbitMqMessage<ActivationUserEmailSendingResultMessageData>
{
    public CorrelationContext CorrelationContext { get; set; }
    public ActivationUserEmailSendingResultMessageData Data { get; set; }
}