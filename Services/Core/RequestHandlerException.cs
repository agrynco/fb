using Services.Exceptions;

namespace Services.Core;

public class RequestHandlerException : ServiceException
{
    public RequestHandlerException(object request, Exception innerException)
        : base($"Error handling request: {request}", innerException)
    {
        Request = request;
    }
    
    public object Request { get; }
}