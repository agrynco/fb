namespace Common.ToStringConverters;

public class StringToStringConverter : BaseToStringConverter<string>
{
    public override string Convert(string? value)
    {
        value = value?.Replace("'", "''");

        return $"'{value}'";
    }
}