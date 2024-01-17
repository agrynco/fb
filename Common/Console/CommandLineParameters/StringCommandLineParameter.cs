namespace Common.Console.CommandLineParameters;

public class StringCommandLineParameter : BaseCommandLineParameter<string>
{
    public StringCommandLineParameter(string name) : base(name)
    {
    }

    public StringCommandLineParameter(string name, string example) : base(name, example)
    {
    }

    protected override string DoParseValueFromString(string value)
    {
        return value;
    }
}