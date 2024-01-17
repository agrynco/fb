namespace Services.RabbitMq;

using Domain;

public interface ISendMessageFromMailSender
{
    public int EmailId { get; set; }
    public int UserId { get; set; }
    
    public EmailSendingStatus SendingStatus { get; set; }
}