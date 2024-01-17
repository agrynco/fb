namespace Common.Console.CommandLineParameters;

public class BooleanCommandLineParameter : BaseCommandLineParameter<bool>
{
    public BooleanCommandLineParameter(string name, string example, bool required) : base(name, example, required)
    {
    }

    public BooleanCommandLineParameter(string name) : base(name)
    {
    }

    public BooleanCommandLineParameter(string name, string example) : base(name, example)
    {
    }

    protected override bool DoParseValueFromString(string value)
    {
        return bool.Parse(value);
    }
}