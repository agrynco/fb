namespace Common.Exceptions;

[Serializable]
public class AssemblyScannerException : Exception
{
    public AssemblyScannerException()
    {
    }

    public AssemblyScannerException(string message)
        : base(message)
    {
    }

    public AssemblyScannerException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}