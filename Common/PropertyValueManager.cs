using System.Reflection;
using Common.Exceptions;

namespace Common;

public class PropertyValueManager
{
    public static readonly char PROPERTY_SEPARATOR = '.';

    public static object? GetValue(string fullPropertyName, object source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source), "Parameter 'source' must be different from null");
        }

        PropertyInfoAndObj propertyInfoAndObj =
            GetPropertyPropertyInfoAndObjForFinalProperty(source, fullPropertyName);

        return propertyInfoAndObj.PropertyInfo.GetValue(propertyInfoAndObj.OwnerOfProperty, null);
    }

    private static PropertyInfoAndObj GetPropertyPropertyInfoAndObjForFinalProperty(object obj,
        string fullPropertyName)
    {
        object referenceToOwnerOfProperty = GetReferenceToPropertyOwner(fullPropertyName, obj)!;
        string[] pathToPropertyParts = fullPropertyName.Split(PROPERTY_SEPARATOR);

        string newPropertyName = pathToPropertyParts.Length > 1
            ? pathToPropertyParts[^1]
            : fullPropertyName;

        PropertyInfo? propertyInfo = referenceToOwnerOfProperty.GetType().GetProperty(newPropertyName);

        if (propertyInfo == null)
        {
            throw new ThereIsNoPropertyException(fullPropertyName, obj);
        }

        return new PropertyInfoAndObj(propertyInfo, referenceToOwnerOfProperty);
    }

    private static object? GetReferenceToPropertyOwner(string pathToProperty, object source)
    {
        string[] pathToPropertyParts = pathToProperty.Split(PROPERTY_SEPARATOR);
        if (pathToPropertyParts.Length > 1)
        {
            string ownerOfProperty = string.Join(PROPERTY_SEPARATOR.ToString(), pathToPropertyParts, 0,
                pathToPropertyParts.Length - 1);

            return GetValue(ownerOfProperty, source);
        }

        return source;
    }

    private sealed class PropertyInfoAndObj
    {
        public PropertyInfoAndObj(PropertyInfo propertyInfo, object ownerOfProperty)
        {
            PropertyInfo = propertyInfo;
            OwnerOfProperty = ownerOfProperty;
        }

        public object OwnerOfProperty { get; }

        public PropertyInfo PropertyInfo { get; }
    }
}