using AboExtensions.Strings;

namespace AboExtensions.Reflections;

public static class ReflectionExtensions
{
    public static object GetPropertyByName(this object o, string property)
    {
        var prop = o.GetType().GetProperty(property)
            ?? throw new ArgumentException($"Property '{property}' not found on type {o.GetType().Name}", nameof(property));
        return prop.GetValue(o, null);
    }
    public static string GetPropertiesToString(this object o, string propNames, char inSep = ',', char outSep = ' ')
    {
        if (!propNames.Contains(inSep))
            return GetPropertyByName(o, propNames).ToString();

        var props = propNames.Split(inSep)
            .Where(s => !s.IsNullOrWhiteSpace())
            .Select(s => s.Trim())
            .Select(s => GetPropertyByName(o, s))
            .ToList();

        return string.Join(outSep, props);
    }
}

