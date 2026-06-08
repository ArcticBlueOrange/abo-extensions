namespace AboExtensions.Randoms;

public static class RandomExtensions
{
    public static bool NextBool(this Random r) => r.Next(2) == 0;
    // TODO: NextBool(this Random rng) : bool
    //   Descrizione: restituisce true o false con probabilità 50/50.
    //   Equivale a rng.Next(2) == 0.
    //   Esempi: new Random().NextBool()  → true o false

    public static bool NextBool(this Random r, double probability)
    {
        var res = r.NextDouble();
        return res <= probability;
    }
    // TODO: NextBool(this Random rng, double probability) : bool
    //   Descrizione: restituisce true con la probabilità indicata (0.0–1.0).
    //   Lancia ArgumentOutOfRangeException se probability non è in [0,1].
    //   Esempi: new Random().NextBool(0.9)  → true nel 90% dei casi
    //           new Random().NextBool(0.0)  → sempre false
    //           new Random().NextBool(1.0)  → sempre true

    // TODO: NextItem<T>(this Random rng, IList<T> list) : T
    //   Descrizione: restituisce un elemento casuale dalla lista.
    //   Lancia ArgumentException se la lista è vuota.
    //   Esempi: new Random().NextItem(new[]{"a","b","c"})  → "a", "b" o "c"

    // TODO: NextEnum<T>(this Random rng) : T  where T : struct, Enum
    //   Descrizione: restituisce un valore casuale dell'enum T.
    //   Esempi: new Random().NextEnum<DayOfWeek>()  → uno dei 7 giorni

    // TODO: NextString(this Random rng, int length, string chars = "abcdefghijklmnopqrstuvwxyz0123456789") : string
    //   Descrizione: genera una stringa casuale di lunghezza length usando i caratteri
    //   del pool chars. Utile per token, password temporanee, slug casuali.
    //   Parametri: length — lunghezza della stringa; chars — pool di caratteri (default alfanumerico).
    //   Esempi: new Random().NextString(8)              → es. "k4f2m9xr"
    //           new Random().NextString(4, "AEIOU")     → es. "OEUA"
}
