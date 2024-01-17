namespace Common.ToStringConverters;

public class DbNullToStringConverter : BaseToStringConverter<DBNull>
{
    public override string Convert(DBNull value)
    {
        return string.Empty;
    }
}