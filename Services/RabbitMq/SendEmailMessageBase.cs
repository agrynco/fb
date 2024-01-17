namespace Services.RabbitMq;

public interface  ISendEmailMessageToMailSender
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string To { get; set; }
}