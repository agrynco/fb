namespace MailSender;

using Common;

public class CorrelationContextAccessor : ICorrelationContextAccessor
{
    private readonly AsyncLocal<ICorrelationContext> _correlationContext = new();

    public CorrelationContext CorrelationContext
    {
        set => _correlationContext.Value = value;
    }

    public ICorrelationContext GetCorrelationContext()
    {
        return _correlationContext.Value;
    }

    public void SetCorrelationContext(ICorrelationContext correlationContext)
    {
        _correlationContext.Value = correlationContext;
    }
}