namespace Common.ToStringConverters;

public interface IParamValueToStringConverter
{
    string? Convert(object value);
}