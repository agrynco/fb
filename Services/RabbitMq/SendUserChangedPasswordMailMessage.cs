namespace Services.RabbitMq;

using Common;
using Domain;
using Services.RabbitMq.Core;

public record SendUserChangedPasswordMailMessageData : ISendEmailMessageToMailSender, ISendMessageFromMailSender
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string To { get; set; }
    public int EmailId { get; set; }
    public int UserId { get; set; }
    public EmailSendingStatus SendingStatus { get; set; }
}

public record SendUserChangedPasswordMailMessage : IRabbitMqMessage<SendUserChangedPasswordMailMessageData>
{
    public CorrelationContext CorrelationContext { get; set; }
    public SendUserChangedPasswordMailMessageData Data { get; set; }
}