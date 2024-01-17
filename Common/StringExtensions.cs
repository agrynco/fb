namespace Common;

public static class StringExtensions
{
    public static string ToLowerTheFirstChar(this string str)
    {
        return Char.ToLower(str[0]) + str.Substring(1);
    }
}