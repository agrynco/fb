namespace Common.Console;

public static class ConsoleExtensions
{
    public static void DoWriteException(Exception ex)
    {
        WriteError("(" + ex.Message);

        if (ex.InnerException != null)
        {
            DoWriteException(ex.InnerException);
        }

        WriteError(")");
    }

    public static string? ReadLine()
    {
        return System.Console.ReadLine();
    }

    public static void WriteError(string text)
    {
        WriteText(text, ConsoleColor.Red, ConsoleColor.Black);
    }

    public static void WriteError(string text, Exception ex)
    {
        WriteError(text);
        DoWriteException(ex);
    }

    public static void WriteException(Exception ex)
    {
        if (ex.InnerException == null)
        {
            WriteError(ex.Message);
        }
        else
        {
            WriteError(ex.Message, ex.InnerException);
        }
    }

    public static void WriteInfo(string text, bool newLineAfter = true)
    {
        WriteText(text, ConsoleColor.Green, ConsoleColor.Black, newLineAfter);
    }

    public static void WriteText(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor,
        bool newLineAfter = true)
    {
        System.Console.BackgroundColor = backgroundColor;
        System.Console.ForegroundColor = foregroundColor;

        System.Console.Write(text);
        if (newLineAfter)
        {
            System.Console.WriteLine();
        }

        System.Console.ResetColor();
    }

    public static void WriteWarning(string text, bool newLineAfter = true)
    {
        WriteText(text, ConsoleColor.Yellow, ConsoleColor.Black, newLineAfter);
    }
}