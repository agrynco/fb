namespace Common.Console.CommandLineParameters;

public class IntCommandLineParameter : BaseCommandLineParameter<int>
{
    public IntCommandLineParameter(string name) : base(name)
    {
    }

    public IntCommandLineParameter(string name, string example) : base(name, example)
    {
    }

    protected override int DoParseValueFromString(string value)
    {
        return int.Parse(value);
    }
}