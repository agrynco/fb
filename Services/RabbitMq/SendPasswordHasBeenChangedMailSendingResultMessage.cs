namespace Services.RabbitMq;

using Common;
using Domain;
using Services.RabbitMq.Core;

public record SendPasswordHasBeenChangedMailSendingResultMessageData : ISendMessageFromMailSender
{
    public int EmailId { get; set; }
    public int UserId { get; set; }
    public EmailSendingStatus SendingStatus { get; set; }
}

public record SendPasswordHasBeenChangedMailSendingResultMessage :
    IRabbitMqMessage<SendPasswordHasBeenChangedMailSendingResultMessageData>
{
    public CorrelationContext CorrelationContext { get; set; }
    public SendPasswordHasBeenChangedMailSendingResultMessageData Data { get; set; }
}