using AboExtensions.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboExtensions.Reflections;

public static class Reflections
{
    public static object GetPropertyByName(this object o, string property)
    {
        return o.GetType().GetProperty(property).GetValue(o, null);
    }
    public static string GetPropertiesToString(object o, string propNames, char inSep = ',', char outSep = ' ')
    {
        if (!propNames.Contains(inSep))
            return GetPropertyByName(o, propNames).ToString();

        var props = propNames.Split(inSep)
            .Where(s => !s.IsNullOrWhiteSpace())
            .Select(s => GetPropertyByName(o, s))
            .ToList();

        return string.Join(outSep, props);
    }
}

