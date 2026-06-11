using AboExtensions.Chars;
using AboExtensions.Objects;
using System.Text;

namespace AboExtensions.Exceptions;

public static class ExceptionExtensions
{
    public static Exception GetRootCause(this Exception e)
    {
        var ret = e;
        while (ret.InnerException != null)
            ret = ret.InnerException;
        return ret;
    }
    public static IEnumerable<Exception> Flatten(this Exception e)
    {
        var v = e;
        yield return v;
        while (v.InnerException != null)
        {
            v = v.InnerException;
            yield return v;
        }
    }
    public static string ToLogString(this Exception e)
    {
        StringBuilder sb = new();
        var spaces = 0;
        sb.Append('[')
            .Append(e.GetType().Name)
            .Append(']')
            .Append(e.Message)
            .Append(e.ToString())
            ;
        foreach (var f in e.Flatten().ToList()[1..])
            sb
              .Append('\n')
              .Append(' '.Repeat(spaces * 4))
              .Append("--->")
              .Append('[')
              .Append(f.GetType().Name)
              .Append(']')
              .Append(f.Message)
              .Also(_ => { spaces++; return true; });

        return sb.ToString();
    }

    public static bool IsOfType<T>(this Exception exception) where T : Exception => exception is T;
}
