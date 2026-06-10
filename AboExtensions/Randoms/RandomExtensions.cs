using AboExtensions.Enums;
using System.Text;

namespace AboExtensions.Randoms;

public static class RandomExtensions
{
    public static bool NextBool(this Random r) => r.Next(2) == 0;

    public static bool NextBool(this Random r, double probability)
    {
        if (probability < 0 || probability > 1)
            throw new ArgumentOutOfRangeException(nameof(probability));

        var res = r.NextDouble();
        return res <= probability;
    }

    public static T NextItem<T>(this Random r, IList<T> l)
    {
        if (l.Count == 0)
            throw new ArgumentException("Empty list");
        var i = r.Next(l.Count);
        return l[i];
    }

    public static T RandomItem<T>(this IList<T> l)
    {
        if (l.Count == 0)
            throw new ArgumentException("Empty list");
        var r = new Random();
        return r.NextItem(l);
    }

    public static T NextEnum<T>(this Random r) where T : struct, Enum
    {
        var vals = EnumExtensions.GetValues<T>().ToList();
        return r.NextItem<T>(vals);
    }

    /// <summary>
    /// Generates a random string of lenght 'len'
    /// </summary>
    /// <param name="r"></param>
    /// <param name="len"></param>
    /// <param name="chars"></param>
    /// <returns></returns>
    public static string NextString(this Random r,
        int len,
        string chars = "abcdefghijklmnopqrstuvwxyz0123456789-ABCDEFGHIJKLMNOPQRSTUVWXYZ_")
    {
        StringBuilder sb = new();
        var cl = chars.ToList();

        for (int i = 0; i < len; i++)
            sb.Append(r.NextItem(cl));

        return sb.ToString();
    }
}
