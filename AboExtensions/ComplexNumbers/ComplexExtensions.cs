using System.Numerics;
using System.Text;

namespace AboExtensions.ComplexNumbers;

public static class ComplexExtensions
{
    public static string ToMathString(this Complex c)
    {
        if (c == 0) return "0";

        StringBuilder sb = new StringBuilder();

        if (c.Real != 0)
            sb.Append(c.Real);

        if (c.Imaginary == 1)
        {
            if (sb.Length > 0)
                sb.Append("+i");
            else
                sb.Append("i");
        }
        else if (c.Imaginary == -1)
            sb.Append("-i");
        else if (c.Imaginary != 0)
        {
            if (sb.Length > 0 && c.Imaginary > 0)
                sb.Append('+');
            sb.Append(c.Imaginary);
            sb.Append('i');
        }
        return sb.ToString();
    }
}
