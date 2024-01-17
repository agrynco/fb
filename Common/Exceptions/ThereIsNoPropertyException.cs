namespace Common.Exceptions;

[Serializable]
public class ThereIsNoPropertyException : Exception
{
    public ThereIsNoPropertyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ThereIsNoPropertyException(string message) : base(message)
    {
    }

    public ThereIsNoPropertyException(string propertyName, object obj)
        : this($"Type {obj.GetType()} dose not contain property '{propertyName}'")
    {
    }

    public ThereIsNoPropertyException(IEnumerable<string> propertyNames, object obj)
        : this($"Type {obj.GetType()} dose not contain properties '{string.Join(", ", propertyNames)}'")
    {
    }
}