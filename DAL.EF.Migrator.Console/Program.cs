using System.Text;
using Common.Console;
using Common.Console.CommandLineParameters;
using MySqlConnector;

namespace DAL.EF.Migrator.Console;

internal static class Program
{
    private static readonly ICommandLineParameter<string> _ENVIRONMENT_NAME =
        new StringCommandLineParameter("environmentName", @"developing");

    private static readonly ICommandLineParameter<string> _PATH_TO_CONFIGS =
        new StringCommandLineParameter("pathToConfigs", @"C:\Temp");

    private static readonly ICommandLineParameter<bool> _CLEAN_DB =
        new BooleanCommandLineParameter("cleanDb", @"[True|False]", false);

    private static readonly ICommandLineParameter<bool> _FILL_DATA =
        new BooleanCommandLineParameter("fillData", @"[True|False]", false);

    private static readonly ICommandLineParameter[] _COMMAND_LINE_PARAMETERS =
    {
        _PATH_TO_CONFIGS, _ENVIRONMENT_NAME, _CLEAN_DB, _FILL_DATA
    };

    private static readonly ICommandLineParameter[][] _SET_OF_SEQUENCES_OF_COMMAND_LINE_PARAMETERS =
    {
        _COMMAND_LINE_PARAMETERS
    };

    private static void LogWrongParams(string title, IEnumerable<ICommandLineParameter> commandLineParameters)
    {
        ConsoleExtensions.WriteWarning(title);

        foreach (ICommandLineParameter commandLineParameter in commandLineParameters)
        {
            ConsoleExtensions.WriteInfo(commandLineParameter.ToString()!);
        }

        System.Console.WriteLine();
    }

    public static int Main(string[] args)
    {
        CommandLineParametersProcessor.LogParamsInfo(args);

        CommandLineProcessingResult processingResult =
            CommandLineParametersProcessor.Process(args, _COMMAND_LINE_PARAMETERS);

        if (!processingResult.IsValid)
        {
            LogWrongParams("Absent parameters: ", processingResult.AbsentCommandLineParameters);
            LogWrongParams("Incomplete parameters: ", processingResult.IncompleteCommandLineParameters);

            CommandLineParametersProcessor.PrintCommandLineParamsInfo(
                new SetOfSequencesOfCommandLineParameters
                {
                    Set = _SET_OF_SEQUENCES_OF_COMMAND_LINE_PARAMETERS
                });

            return 1;
        }

        try
        {
            DbMigrator.BeforeMigrate += (pathToConfigs, environmentName, connectionString) =>
            {
                var sb = new StringBuilder(PropertyValueToString(nameof(pathToConfigs), pathToConfigs));
                sb.Append("; " + PropertyValueToString(nameof(environmentName), environmentName));
                sb.Append("; " + PropertyValueToString(nameof(connectionString), connectionString));

                System.Console.WriteLine(sb.ToString());
                System.Console.WriteLine();
            };

            int triesCount = 30;
            for (int triesCounter = 1; triesCounter <= triesCount; triesCounter++)
            {
                try
                {
                    System.Console.WriteLine($"Try {triesCounter}");

                    DbMigrator.Migrate(_PATH_TO_CONFIGS.Value!, _ENVIRONMENT_NAME.Value!, _CLEAN_DB.Value,
                        _FILL_DATA.Value);

                    System.Console.WriteLine("successful");

                    return 0;
                }
                catch (MySqlException e)
                {
                    System.Console.Write("failure. ");
                    ConsoleExtensions.WriteException(e);

                    if (triesCounter < triesCount)
                    {
                        System.Console.WriteLine("Waiting 2 seconds before next try...");
                        Thread.Sleep(2000);
                    }

                    System.Console.WriteLine();
                }
            }

            return 2;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            return 1;
        }
    }

    private static string PropertyValueToString(string propertyName, object? value)
    {
        string sValue = value == null ? string.Empty : value.ToString()!;
        return $"{propertyName} = {sValue}";
    }
}