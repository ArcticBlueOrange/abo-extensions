using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboExtensions.Numbers;

public static class NumberExtensions
{
    public static float Or(this float? f, float o = 0) => f ?? o;
    public static double Round(this double d, int dec) => Math.Round(d, dec);
    public static decimal Round(this decimal d, int dec) => Math.Round(d, dec);
    public static bool IsNanOrInf(this float f) => float.IsNaN(f) || float.IsInfinity(f);
    public static bool IsNotNanNorInf(this float f) => !f.IsNanOrInf();
}

