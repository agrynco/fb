namespace DI;

[Serializable]
public class ReplaceServiceException : Exception
{
    public ReplaceServiceException()
    {
    }

    public ReplaceServiceException(string message) : base(message)
    {
    }

    public ReplaceServiceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}