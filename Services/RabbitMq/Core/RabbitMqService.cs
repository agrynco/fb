namespace Services.RabbitMq.Core;

using System.Text;
using System.Text.Json;
using Common;
using RabbitMQ.Client;
using Serilog;

public class RabbitMqService(
    RabbitMqSettings settings,
    ILogger logger,
    ICorrelationContextAccessor correlationContextAccessor)
    : IRabbitMqService
{
    private readonly ConnectionFactory _connectionFactory = new()
    {
        Uri = settings.Build()
    };

    public void SendMessage(MessageQueues messageQueue, object obj)
    {
        ICorrelationContext correlationContext = correlationContextAccessor.GetCorrelationContext();

        var message = JsonSerializer.Serialize(new
        {
            CorrelationContext = correlationContext,
            Data = obj
        });
        
        logger.Information($"Sending message to {messageQueue} queue: {message}");

        SendMessage(messageQueue, message);
    }

    private void SendMessage(MessageQueues messageQueue, string message)
    {
        using IConnection? connection = _connectionFactory.CreateConnection();

        using IModel? channel = connection.CreateModel();

        string routingKey = messageQueue.ToString();

        channel.QueueDeclare(queue: routingKey,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        byte[] body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
            routingKey: routingKey,
            basicProperties: null,
            body: body);
    }
}