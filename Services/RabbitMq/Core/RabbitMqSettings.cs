namespace Services.RabbitMq.Core;

public record RabbitMqSettings
{
    public static readonly string SectionName = "RabbitMq";
    public string Host { get; set; } = null!;
    public string Password { get; set; } = null!;
    public uint Port { get; set; }
    public string Username { get; set; } = null!;

    private string BuildConnectionString()
    {
        return $"amqp://{Username}:{Password}@{Host}:{Port}";
    }

    public Uri Build()
    {
        return new Uri(BuildConnectionString());
    }
}