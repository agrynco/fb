using System.Reflection;
using System.Text;
using Common.Exceptions;

namespace Common.Console.CommandLineParameters;

public class SetOfSequencesOfCommandLineParameters
{
    public ICommandLineParameter[][] Set { get; set; } = Array.Empty<ICommandLineParameter[]>();
}

public static class CommandLineParametersProcessor
{
    public static void PrintCommandLineParamsInfo(
        SetOfSequencesOfCommandLineParameters setOfSequencesOfCommandLineParameters)
    {
        var exeFileInfo = new FileInfo(Assembly.GetEntryAssembly()!.Location);

        var stringBuilder = new StringBuilder();
        foreach (var sequenceOfCoCommandLineParameters in setOfSequencesOfCommandLineParameters.Set)
        {
            stringBuilder.Append(exeFileInfo.Name).Append(' ');
            foreach (ICommandLineParameter commandLineParameter in sequenceOfCoCommandLineParameters)
            {
                stringBuilder.Append(commandLineParameter.BuildExample()).Append("");
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(' ');
                }
            }

            stringBuilder.AppendLine();
        }

        ConsoleExtensions.WriteInfo(stringBuilder.ToString());
    }

    public static void LogParamsInfo(string[] args)
    {
        if (args.Length == 0)
        {
            return;
        }

        System.Console.WriteLine($"Passed arguments number: {args.Length}");

        for (int i = 0; i < args.Length; i++)
        {
            System.Console.WriteLine($"arg[{i + 1}] = {args[i]}");
        }

        System.Console.WriteLine();
    }

    public static CommandLineProcessingResult Process(IEnumerable<string> args,
        IEnumerable<ICommandLineParameter> commandLineParameters)
    {
        var completeCommandLineParameters = new List<ICommandLineParameter>();
        var presentCommandLineParameters = new List<ICommandLineParameter>();
        var incompleteCommandLineParameters = new List<ICommandLineParameter>();

        var lineParameters = commandLineParameters as ICommandLineParameter[] ?? commandLineParameters.ToArray();

        foreach (string arg in args)
        {
            int indexOfFirstEqual = arg.IndexOf("=", StringComparison.Ordinal);

            if (indexOfFirstEqual == -1)
            {
                throw new NotSupportedException();
            }

            string paramName = arg[..indexOfFirstEqual];
            string paramValue = arg[(indexOfFirstEqual + 1)..];

            ICommandLineParameter? commandLineParameter =
                lineParameters.SingleOrDefault(x => x.Name == paramName);

            if (commandLineParameter == null)
            {
                continue;
            }

            presentCommandLineParameters.Add(commandLineParameter);

            try
            {
                commandLineParameter.ParseValueFromString(paramValue);
                completeCommandLineParameters.Add(commandLineParameter);
            }
            catch (ParseCommandLineParameterException)
            {
                if (commandLineParameter.Required)
                {
                    incompleteCommandLineParameters.Add(commandLineParameter);
                }
            }
        }

        var absentCommandLineParameters = lineParameters.Except(presentCommandLineParameters)
            .Where(parameter => parameter.Required);

        return new CommandLineProcessingResult(absentCommandLineParameters, incompleteCommandLineParameters,
            completeCommandLineParameters);
    }
}