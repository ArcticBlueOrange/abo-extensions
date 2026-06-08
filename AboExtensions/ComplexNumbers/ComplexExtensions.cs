using System.Numerics;

namespace AboExtensions.ComplexNumbers;

public static class ComplexExtensions
{
    // TODO: ToMathString(this Complex c) : string
    //   Descrizione: formatta un numero complesso nella notazione matematica standard "a+bi".
    //   Omette la parte reale se zero, omette la parte immaginaria se zero,
    //   omette il coefficiente 1 davanti a "i" (es. "i" invece di "1i").
    //   Parametri: nessuno oltre al valore.
    //   Esempi: new Complex(1, 2).ToMathString()    → "1+2i"
    //           new Complex(1, -2).ToMathString()   → "1-2i"
    //           new Complex(0, 2).ToMathString()    → "2i"
    //           new Complex(1, 0).ToMathString()    → "1"
    //           new Complex(0, 0).ToMathString()    → "0"
    //           new Complex(0, 1).ToMathString()    → "i"
    //           new Complex(0, -1).ToMathString()   → "-i"
    //           new Complex(1, 1).ToMathString()    → "1+i"
    //           new Complex(1, -1).ToMathString()   → "1-i"
    //           new Complex(-1, 2).ToMathString()   → "-1+2i"
    //   Nota: il ToString() di default di Complex produce "(1; 2)" — non standard.
}
