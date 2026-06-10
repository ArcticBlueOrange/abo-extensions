using System.Text;

namespace AboExtensions.StringBuilders;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendIf(this StringBuilder builder, bool condition, object text)
    {
        if (condition)
            builder.Append(text);
        return builder;
    }

    public static StringBuilder AppendLineIf(this StringBuilder builder, bool condition, string text)
    {
        if (condition)
            builder.AppendLine(text);
        return builder;
    }

    public static bool IsEmpty(this StringBuilder builder) => builder.Length == 0;

    public static StringBuilder Prepend(this StringBuilder sb, object text)
    {
        sb.Insert(0, text);
        return sb;
    }
}
