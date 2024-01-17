using System.Reflection;

namespace Common.Exceptions;

[Serializable]
public class CanNotAssignPropertyException : Exception
{
    public CanNotAssignPropertyException(PropertyInfo sourceProperty, Type destinationType, Exception innerException)
        : base("", innerException)
    {
        SourceProperty = sourceProperty;
        DestinationType = destinationType;
    }

    public Type DestinationType { get; }

    public PropertyInfo SourceProperty { get; }
}