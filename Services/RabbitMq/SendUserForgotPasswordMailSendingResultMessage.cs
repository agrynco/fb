namespace Services.RabbitMq;

using Common;
using Domain;
using Services.RabbitMq.Core;

public record SendUserForgotPasswordMailSendingResultMessageData : ISendMessageFromMailSender
{
    public int EmailId { get; set; }
    public int UserId { get; set; }
    public EmailSendingStatus SendingStatus { get; set; }
}

public record SendUserForgotPasswordMailSendingResultMessage 
    : IRabbitMqMessage<SendUserForgotPasswordMailSendingResultMessageData>
{
    public CorrelationContext CorrelationContext { get; set; }
    public SendUserForgotPasswordMailSendingResultMessageData Data { get; set; }
}