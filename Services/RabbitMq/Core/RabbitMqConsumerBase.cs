namespace Services.RabbitMq.Core;

using System.Text;
using System.Text.Json;
using Common;
using Logging;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using Serilog.Context;

public interface IRabbitMqMessage<TMessageData>
{
    public CorrelationContext CorrelationContext { get; set; }
    public TMessageData Data { get; set; }
}

public abstract class RabbitMqConsumerBase<TMessage, TMessageData> : BackgroundService
    where TMessage : IRabbitMqMessage<TMessageData>
{
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly ILogger _contextualLogger;
    private readonly MessageQueues _messageQueue;
    private readonly ICorrelationContextAccessor _correlationContextAccessor;

    protected RabbitMqConsumerBase(RabbitMqSettings settings,
        IContextualSerilogLogger<RabbitMqConsumerBase<TMessage, TMessageData>> contextualLogger,
        MessageQueues messageQueue,
        ICorrelationContextAccessor correlationContextAccessor)
    {
        _contextualLogger = contextualLogger.Logger;
        _messageQueue = messageQueue;
        _correlationContextAccessor = correlationContextAccessor;
        ConnectionFactory connectionFactory = new()
        {
            Uri = settings.Build()
        };

        _connection = connectionFactory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(messageQueue.ToString(),
            false,
            false,
            false,
            null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (_, ea) =>
        {
            var body = ea.Body;
            string message = Encoding.UTF8.GetString(body.ToArray());

            var messageObj = JsonSerializer.Deserialize<TMessage>(message)!;
            _correlationContextAccessor.SetCorrelationContext(messageObj.CorrelationContext);
            
            LogContext.PushProperty("CorrelationId", messageObj.CorrelationContext.CorrelationId);
            _contextualLogger.Information("{Sender}: Received {@Message}", this.GetType().Name, message);

            await HandleMessage(messageObj);
        };

        _channel.BasicConsume(
            _messageQueue.ToString(),
            true,
            consumer);

        return Task.CompletedTask;
    }

    protected abstract Task HandleMessage(TMessage message);

    public override void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();

        base.Dispose();
    }
}