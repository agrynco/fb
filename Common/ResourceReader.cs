using System.Reflection;
using System.Text;

namespace Common;

public static class ResourceReader
{
    public static Stream GetStream(Type resourceIdentifierType, string name)
    {
        var executingAssembly = Assembly.GetAssembly(resourceIdentifierType)!;

        Stream? stream = executingAssembly.GetManifestResourceStream(name);

        if (stream == null)
        {
            throw new ThereIsNoEmbeddedResourceException($"There is no embedded resource with name '{name}'");
        }

        return stream!;
    }

    public static byte[] ReadAsBytes(Type resourceIdentifierType, string name)
    {
        Stream stream = GetStream(resourceIdentifierType, name);

        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        return buffer;
    }

    public static string ReadAsString(Type resourceIdentifierType, string name)
    {
        return ReadAsString(resourceIdentifierType, name, Encoding.Default);
    }

    public static string ReadAsString<T>(string name)
    {
        return ReadAsString(typeof(T), name);
    }

    public static string ReadAsString(Type resourceIdentifierType, string name, Encoding encoding)
    {
        Stream stream = GetStream(resourceIdentifierType, name);
        var sr = new StreamReader(stream, encoding);
        return sr.ReadToEnd();
    }
}