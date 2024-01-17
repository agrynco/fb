using Common.Exceptions;
using Common.ToStringConverters;

namespace Common.Console.CommandLineParameters;

public abstract class BaseCommandLineParameter<TValue> : BaseClass, ICommandLineParameter<TValue>
{
    protected BaseCommandLineParameter(string name, string example, bool required)
        : this(name, example)
    {
        Required = required;
    }

    protected BaseCommandLineParameter(string name)
        : this(name, string.Empty)
    {
        Name = name;
    }

    protected BaseCommandLineParameter(string name, string example)
    {
        Name = name;
        Example = example;
        Required = true;
    }

    object ICommandLineParameter.Example => Example;

    public string Example { get; }

    public string Name { get; set; }

    object? ICommandLineParameter.Value => ToStringConverter.Instance.Convert(Value);

    public bool Required { get; set; }

    public TValue? Value { get; protected set; }

    public string BuildExample()
    {
        return $"{Name}={Example}";
    }

    public void ParseValueFromString(string value)
    {
        try
        {
            Value = DoParseValueFromString(value);
        }
        catch (Exception ex)
        {
            throw new ParseCommandLineParameterException(Name, typeof(int), value, ex);
        }
    }

    public override string ToString()
    {
        return $"{Name}={Value}";
    }

    protected abstract TValue DoParseValueFromString(string value);
}