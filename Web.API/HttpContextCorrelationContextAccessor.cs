namespace Web.API;

using Common;
using Serilog.AspNetCore.Enrichers;

public class HttpContextCorrelationContextAccessor(
    IHttpContextAccessor httpContextAccessor)
    : ICorrelationContextAccessor
{
    public ICorrelationContext GetCorrelationContext()
    {
        if (httpContextAccessor.HttpContext!.Items.TryGetValue(
                $"{nameof(CorrelationIdEnricher)}+CorrelationId", out object? correlationId))
        {
            if (correlationId is string correlationIdString)
            {
                return new CorrelationContext
                {
                    CorrelationId = correlationIdString
                };
            }

            throw new InvalidOperationException(
                $"CorrelationId is not a string: {correlationId.GetType()}");
        }

        return null;
    }

    public void SetCorrelationContext(ICorrelationContext correlationContext)
    {
    }
}