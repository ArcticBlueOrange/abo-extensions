using System.Numerics;
using System.Text;

namespace AboExtensions.ComplexNumbers;

public static class ComplexExtensions
{
    public static string ToMathString(this Complex c)
    {
        if (c == 0) return "0";

        StringBuilder sb = new();

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

    // TODO: ToPolar(this Complex c) : (double Magnitude, double Phase)
    //   Descrizione: restituisce la rappresentazione polare come tupla (magnitudine, fase in radianti).
    //   Esempi: new Complex(0, 1).ToPolar() → (1.0, π/2)
    //           new Complex(1, 0).ToPolar() → (1.0, 0.0)

    // TODO: Rotate(this Complex c, double angle) : Complex
    //   Descrizione: ruota il numero complesso di angle radianti (moltiplica per e^(i*angle)).
    //   Usato in grafica 2D e signal processing.
    //   Esempi: new Complex(1, 0).Rotate(Math.PI / 2) → ~(0, 1)

    public static bool IsReal(this Complex c) => c.Imaginary == 0;

    public static bool IsImaginary(this Complex c) => c.Imaginary != 0 && c.Real == 0;

    // TODO: NthRootsOfUnity(int n) : IEnumerable<Complex>
    //   Descrizione: restituisce le n radici n-esime dell'unità (e^(2πik/n) per k=0..n-1).
    //   Usato in FFT e trasformazioni di Fourier discreta.
    //   Esempi: NthRootsOfUnity(4) → { 1, i, -1, -i }

    public static Vector2 ToVector2(this Complex c) => new Vector2((float)c.Real, (float)c.Imaginary);
}
