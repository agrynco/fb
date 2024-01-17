namespace Common.Console.CommandLineParameters;

public class NullableBooleanCommandLineParameter : BaseCommandLineParameter<bool?>
{
    public NullableBooleanCommandLineParameter(string name, string example, bool required) : base(name, example,
        required)
    {
    }

    public NullableBooleanCommandLineParameter(string name) : base(name)
    {
    }

    public NullableBooleanCommandLineParameter(string name, string example) : base(name, example)
    {
    }

    protected override bool? DoParseValueFromString(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return bool.Parse(value);
        }

        return null;
    }
}