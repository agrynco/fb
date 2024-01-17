namespace Web.API.Exceptions;

[Serializable]
public class CanNotDetectIpAddressException : Exception
{
    public CanNotDetectIpAddressException()
    {
    }

    public CanNotDetectIpAddressException(string message) : base(message)
    {
    }

    public CanNotDetectIpAddressException(string message, Exception innerException) : base(message, innerException)
    {
    }
}