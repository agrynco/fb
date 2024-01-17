using System.Collections;
using System.Reflection;
using System.Text;

namespace Common.ToStringConverters;

public class ToStringConverter : IParamValueToStringConverter
{
    private static ToStringConverter? _INSTANCE;

    private readonly Dictionary<Type, IParamValueToStringConverter> _converters;

    private ToStringConverter()
    {
        _converters = new Dictionary<Type, IParamValueToStringConverter>();
        RegisterAllConverters();
    }

    public static ToStringConverter Instance => _INSTANCE ??= new ToStringConverter();

    public string? Convert(object? value)
    {
        if (value == null)
        {
            return "null";
        }

        Type type = value.GetType();
        type = type.IsEnum ? typeof(Enum) : type;

        if (_converters.TryGetValue(type, out IParamValueToStringConverter? converter)) 
        {
            return converter.Convert(value);
        }

        throw new KeyNotFoundException($"Converter for type '{type}' is not registered.");
    }

    public static string? ConvertClass(object? obj)
    {
        if (obj == null) 
        {
            return Instance.Convert(null);
        }

        var alreadyToStringConverted = new Dictionary<object, object?>();
        return ConvertClass(obj, alreadyToStringConverted);
    }

    private void Register(Type type, IParamValueToStringConverter converter)
    {
        _converters.Add(type, converter);
    }

    private void RegisterAllConverters()
    {
        Register(typeof(Guid), new GuidToStringConverter());
        Register(typeof(int), new BaseToStringConverter<int>());
        Register(typeof(uint), new BaseToStringConverter<uint>());
        Register(typeof(bool), new BaseToStringConverter<bool>());
        Register(typeof(string), new StringToStringConverter());
        Register(typeof(decimal), new BaseToStringConverter<decimal>());
        Register(typeof(long), new BaseToStringConverter<long>());
        Register(typeof(DateTime), new BaseToStringConverter<DateTime>());
        Register(typeof(DBNull), new DbNullToStringConverter());
        Register(typeof(int[]), new EnumerableToStringConverter());
        Register(typeof(Enum), new BaseToStringConverter<Enum>());
        Register(typeof(double), new BaseToStringConverter<double>());
        Register(typeof(char), new BaseToStringConverter<char>());
        Register(typeof(TimeSpan), new BaseToStringConverter<TimeSpan>());
    }

    protected void Unregister(Type type)
    {
        _converters.Remove(type);
    }

    private static string ConvertClass(object obj, Dictionary<object, object?> alreadyToStringConverted)
    {
        if (alreadyToStringConverted.ContainsKey(obj))
        {
            return "Circular reference";
        }

        alreadyToStringConverted.Add(obj, obj);
        var result = new StringBuilder();
        result.Append('(');

        var propertiesToBeConverted = GetPropertiesToBeConverted(obj.GetType());
        foreach (PropertyInfo propertyInfo in propertiesToBeConverted)
        {
            if (result.Length > 1)
            {
                result.Append("; ");
            }

            try
            {
                object? propertyValue = propertyInfo.GetValue(obj, null);

                if (propertyValue is IEnumerable value && IsNotString(propertyInfo.PropertyType))
                {
                    HandleIEnumerableNotString(alreadyToStringConverted, result, propertyInfo, value);
                }
                else
                {
                    ProcessValue(propertyInfo.Name, propertyValue, result, alreadyToStringConverted);
                }
            }
            catch (Exception ex)
            {
                result.Append(result.Append($"{propertyInfo.Name} = {ex.Message}"));
            }
        }

        result.Append(')');
        return result.ToString();
    }

    private static void HandleIEnumerableNotString(Dictionary<object, object?> alreadyToStringConverted,
        StringBuilder result,
        MemberInfo propertyInfo, IEnumerable value)
    {
        result.Append(propertyInfo.Name).Append(" = [");
        const string ITEM_SEPARATOR = ", ";
        foreach (object item in value)
        {
            ProcessValue(null, item, result, alreadyToStringConverted);
            result.Append(ITEM_SEPARATOR);
        }

        if (result.ToString().EndsWith(ITEM_SEPARATOR))
        {
            result.Length -= ITEM_SEPARATOR.Length;
        }

        result.Append(']');
    }

    private static PropertyInfo[] GetPropertiesToBeConverted(Type type)
    {
        var propertyInfos = type.GetProperties();

        return propertyInfos
            .Where(x => x.GetCustomAttributes(typeof(IgnoreConvertToStringAttribute), false).Length == 0).ToArray();
    }

    private static bool IsClass(object? propertyValue)
    {
        return propertyValue != null && !propertyValue.GetType().IsValueType &&
               IsNotString(propertyValue.GetType());
    }

    private static bool IsNotString(Type propertyType)
    {
        return propertyType != typeof(string);
    }

    private static void ProcessValue(string? propertyName, object? propertyValue, StringBuilder result,
        Dictionary<object, object?> alreadyToStringConverted)
    {
        var valuesForFormattedString = new List<string?>();
        string formatPattern;
        if (string.IsNullOrEmpty(propertyName))
        {
            formatPattern = "{0}";
        }
        else
        {
            formatPattern = "{0} = {1}";
            valuesForFormattedString.Add(propertyName);
        }

        valuesForFormattedString.Add(IsClass(propertyValue)
            ? ConvertClass(propertyValue!, alreadyToStringConverted)
            : Instance.Convert(propertyValue));

        // ReSharper disable once CoVariantArrayConversion
        result.Append(string.Format(formatPattern, valuesForFormattedString.ToArray()));
    }
}