#region Usings
using System.Text;
#endregion

namespace Common.Console.CommandLineParameters;

public class CommandLineProcessingResult
{
    public CommandLineProcessingResult(IEnumerable<ICommandLineParameter> absentCommandLineParameters,
        IEnumerable<ICommandLineParameter> incompleteCommandLineParameters,
        IEnumerable<ICommandLineParameter> completeCommandLineParameters)
    {
        AbsentCommandLineParameters = absentCommandLineParameters;
        IncompleteCommandLineParameters = incompleteCommandLineParameters;
        CompleteCommandLineParameters = completeCommandLineParameters;

        IsValid = !AbsentCommandLineParameters.Any() && !IncompleteCommandLineParameters.Any();
    }

    public IEnumerable<ICommandLineParameter> AbsentCommandLineParameters { get; }

    public IEnumerable<ICommandLineParameter> CompleteCommandLineParameters { get; }

    public IEnumerable<ICommandLineParameter> IncompleteCommandLineParameters { get; }

    public bool IsValid { get; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(ToStringCommandLineParameters("Absent parameters are: ",
            AbsentCommandLineParameters));

        stringBuilder.AppendLine(ToStringCommandLineParameters("Complete parameters are: ",
            CompleteCommandLineParameters));

        stringBuilder.AppendLine(ToStringCommandLineParameters("Incomplete parameters are: ",
            IncompleteCommandLineParameters));

        return stringBuilder.ToString();
    }

    private static string ToStringCommandLineParameters(string prefix,
        IEnumerable<ICommandLineParameter> commandLineParameters)
    {
        return $"{prefix} {string.Join("; ", commandLineParameters)}";
    }
}