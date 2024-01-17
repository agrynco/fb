namespace Services.RabbitMq;

using Common;
using Domain;
using Services.RabbitMq.Core;

public record SendUserRegisteredMailMessageData :
    ISendEmailMessageToMailSender,
    ISendMessageFromMailSender
{
    public required string Subject { get; set; }
    public required string Body { get; set; } = default!;
    public required string To { get; set; } = default!;
    public required int EmailId { get; set; }
    public required int UserId { get; set; }
    public EmailSendingStatus SendingStatus { get; set; }
}

public record SendUserRegisteredMailMessage :
    IRabbitMqMessage<SendUserRegisteredMailMessageData>
{
    public CorrelationContext CorrelationContext { get; set; }
    public SendUserRegisteredMailMessageData Data { get; set; }
}