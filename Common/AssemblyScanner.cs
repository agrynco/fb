using System.Reflection;
using Common.Exceptions;

namespace Common;

public static class AssemblyScanner
{
    #region Static Methods (public)
    /// <summary>
    ///     Search specified types in assembly
    /// </summary>
    /// <param name="assembly">Assembly to be scanned</param>
    /// <param name="typeCriteria">Type to search</param>
    /// <returns>Array of types</returns>
    public static Type[] Search(Assembly assembly, Type typeCriteria)
    {
        return Search(assembly, typeCriteria, true, true);
    }

    public static Type[] Search(Assembly assembly, Type typeCriteria, bool excludeInterfaces,
        bool excludeAbstractClasses)
    {
        var result = (from t in assembly.GetTypes()
            where
                (!excludeInterfaces || !t.IsInterface)
                && (!excludeAbstractClasses || !t.IsAbstract)
                && typeCriteria.IsAssignableFrom(t)
            select t).ToArray();

        return result;
    }

    public static Type[] Search(Assembly[] assemblies, Type[] attributeTypes)
    {
        var assembliesToScan = new List<Assembly>();

        assembliesToScan.AddRange(assemblies.Length != 0 ? assemblies : AppDomain.CurrentDomain.GetAssemblies());

        var result = new List<Type>();
        foreach (Assembly assembly in assembliesToScan)
        {
            result.AddRange(Search(assembly, attributeTypes));
        }

        return result.ToArray();
    }

    public static Type[] Search(Assembly assembly, Type[] attributeTypes)
    {
        var result = new List<Type>(assembly.GetTypes());
        for (int i = result.Count - 1; i > -1; i--)
        {
            var customAttributes = new List<object>();
            foreach (Type attributeType in attributeTypes)
            {
                object[] attributes = result[i].GetCustomAttributes(false);
                Type type = attributeType;
                customAttributes.AddRange(attributes.Where(ca => ca.GetType().FullName == type.FullName));
            }

            if (customAttributes.Count == 0)
            {
                result.RemoveAt(i);
            }
        }

        return result.ToArray();
    }

    public static Type[] Search(Type desiredType)
    {
        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
            .Where(type => desiredType.IsAssignableFrom(type) && !desiredType.IsEquivalentTo(type)).ToArray();
    }

    public static Type Search(string fullTypeName)
    {
        Type? result = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(type => type.FullName == fullTypeName);

        if (result == null)
        {
            throw new AssemblyScannerException(
                $"Can not find assembly which contains implementation of {fullTypeName}");
        }

        return result;
    }
    #endregion
}