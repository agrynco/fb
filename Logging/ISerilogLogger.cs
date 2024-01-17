namespace Logging;

using Serilog;

public interface IContextualSerilogLogger<TContext>
{
    ILogger Logger { get; }
}

public class ContextualSerilogLogger<TContext> : IContextualSerilogLogger<TContext>
{
    public ILogger Logger { get; } = Log.ForContext<TContext>();
}