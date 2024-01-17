namespace Common.Console.CommandLineParameters;

public class DateCommandLineParameter : BaseCommandLineParameter<DateTime>
{
    private readonly IFormatProvider? _formatProvider;

    public DateCommandLineParameter(string name) : base(name)
    {
        _formatProvider = null;
    }

    public DateCommandLineParameter(string name, string example, IFormatProvider? formatProvider = null)
        : base(name, example)
    {
        _formatProvider = formatProvider;
    }

    protected override DateTime DoParseValueFromString(string value)
    {
        return _formatProvider == null ? DateTime.Parse(value) : DateTime.Parse(value, _formatProvider);
    }
}