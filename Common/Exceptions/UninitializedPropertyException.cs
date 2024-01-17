namespace Common.Exceptions;

[Serializable]
public class UninitializedPropertyException : Exception
{
    public UninitializedPropertyException(string className, string propertyName)
        : base($"{className}.{propertyName} is uninitialized")
    {
    }
}