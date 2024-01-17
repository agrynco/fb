namespace Common.Exceptions;

[Serializable]
public class ParseCommandLineParameterException : CommandLineParameterException
{
    public ParseCommandLineParameterException(string nameOfCommandLineParameter, Type typeOfValue,
        string stringValueToParse, Exception? ex = null) : base(
        $"Can not parse {nameOfCommandLineParameter} from string = '{stringValueToParse}' " +
        $"to type {typeOfValue} for command line parameter with name = '{stringValueToParse}'", ex)
    {
    }
}