namespace Common.Console.CommandLineParameters;

public interface ICommandLineParameter
{
    object Example { get; }

    string Name { get; set; }

    bool Required { get; set; }
    object? Value { get; }
    string BuildExample();
    void ParseValueFromString(string value);
}

public interface ICommandLineParameter<out TValue> : ICommandLineParameter
{
    new string Example { get; }

    new TValue? Value { get; }
}