namespace Services.RabbitMq;

using Common;
using Domain;
using Services.RabbitMq.Core;

public record SendUserForgotPasswordMailMessageData 
    : ISendEmailMessageToMailSender, 
        ISendMessageFromMailSender
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string To { get; set; }
    public int EmailId { get; set; }
    public int UserId { get; set; }
    public EmailSendingStatus SendingStatus { get; set; }
}

public record SendUserForgotPasswordMailMessage :
    IRabbitMqMessage<SendUserForgotPasswordMailMessageData>
{
    public CorrelationContext CorrelationContext { get; set; }
    public SendUserForgotPasswordMailMessageData Data { get; set; }
}