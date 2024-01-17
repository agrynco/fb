namespace Common;

public interface ICorrelationContext
{
    string CorrelationId { get; }
}

public record CorrelationContext : ICorrelationContext
{
    public required string CorrelationId { get; init; }
}

public interface ICorrelationContextAccessor
{
    ICorrelationContext GetCorrelationContext();
    void SetCorrelationContext(ICorrelationContext correlationContext);
}