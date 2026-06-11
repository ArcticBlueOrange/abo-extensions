using System.Numerics;

namespace AboExtensions.Vectors;

public static class VectorExtensions
{
    // TODO: AngleTo(this Vector2 v, Vector2 other) : float
    //   Descrizione: angolo in radianti tra due vettori 2D (0..π).
    //   Esempi: Vector2.UnitX.AngleTo(Vector2.UnitY) → π/2

    // TODO: Rotate(this Vector2 v, float angle) : Vector2
    //   Descrizione: ruota il vettore di angle radianti attorno all'origine.
    //   Esempi: Vector2.UnitX.Rotate(MathF.PI / 2) → ~Vector2.UnitY

    // TODO: Perpendicular(this Vector2 v) : Vector2
    //   Descrizione: vettore perpendicolare (rotazione di 90°, stessa lunghezza).
    //   Esempi: new Vector2(1, 0).Perpendicular() → new Vector2(0, 1)

    // TODO: ToAngle(this Vector2 v) : float
    //   Descrizione: angolo del vettore rispetto all'asse X positivo (atan2), in radianti (-π..π).
    //   Esempi: Vector2.UnitY.ToAngle() → π/2

    // TODO: ProjectOnto(this Vector2 v, Vector2 onto) : Vector2
    //   Descrizione: proiezione vettoriale di v su onto.
    //   Esempi: new Vector2(1, 1).ProjectOnto(Vector2.UnitX) → Vector2.UnitX

    // TODO: ClampMagnitude(this Vector2 v, float max) : Vector2
    //   Descrizione: limita la lunghezza del vettore a max, mantenendo la direzione.
    //   Esempi: new Vector2(3, 4).ClampMagnitude(1f) → new Vector2(0.6f, 0.8f)

    // TODO: ApproxEquals(this Vector2 v, Vector2 other, float epsilon = 1e-6f) : bool
    //   Descrizione: uguaglianza float-safe entro epsilon per componente.
    //   Esempi: new Vector2(1f, 0f).ApproxEquals(new Vector2(1.0000001f, 0f)) → true

    // TODO: IsParallelTo(this Vector3 v, Vector3 other, float epsilon = 1e-6f) : bool
    //   Descrizione: true se i vettori sono paralleli (prodotto vettoriale ≈ zero).
    //   Esempi: Vector3.UnitX.IsParallelTo(new Vector3(5, 0, 0)) → true

    // TODO: IsPerpendicularTo(this Vector3 v, Vector3 other, float epsilon = 1e-6f) : bool
    //   Descrizione: true se i vettori sono perpendicolari (prodotto scalare ≈ 0).
    //   Esempi: Vector3.UnitX.IsPerpendicularTo(Vector3.UnitY) → true

    // TODO: ToComplex(this Vector2 v) : System.Numerics.Complex
    //   Descrizione: converte in Complex (X→Real, Y→Imaginary).
    //   Esempi: new Vector2(3f, 4f).ToComplex() → new Complex(3, 4)
}
