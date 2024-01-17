namespace Services.RabbitMq.Core;

public interface IRabbitMqService
{
    void SendMessage(MessageQueues messageQueue, object obj);
}