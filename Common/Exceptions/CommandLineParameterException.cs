namespace Common.Exceptions;

[Serializable]
public class CommandLineParameterException : Exception
{
    public CommandLineParameterException()
    {
    }

    public CommandLineParameterException(string message)
        : base(message)
    {
    }

    public CommandLineParameterException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
}