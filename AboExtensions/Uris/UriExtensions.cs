namespace AboExtensions.Uris;

public static class UriExtensions
{
    // TODO: IsAbsolute(this Uri uri) : bool
    //   Descrizione: true se l'URI è assoluto (ha scheme + host).
    //   Esempi: new Uri("https://example.com").IsAbsolute() → true
    //           new Uri("/path/to/page", UriKind.Relative).IsAbsolute() → false

    // TODO: IsRelative(this Uri uri) : bool
    //   Descrizione: true se l'URI è relativo. Equivale a !IsAbsolute().
    //   Esempi: new Uri("/path", UriKind.Relative).IsRelative() → true

    // TODO: IsHttps(this Uri uri) : bool
    //   Descrizione: true se lo scheme è "https" (case-insensitive).
    //   Esempi: new Uri("https://example.com").IsHttps() → true
    //           new Uri("http://example.com").IsHttps()  → false

    // TODO: IsHttp(this Uri uri) : bool
    //   Descrizione: true se lo scheme è "http" o "https".
    //   Esempi: new Uri("http://example.com").IsHttp()  → true
    //           new Uri("ftp://example.com").IsHttp()   → false

    // TODO: GetDomain(this Uri uri) : string
    //   Descrizione: restituisce host senza "www." iniziale se presente.
    //   Esempi: new Uri("https://www.example.com/path").GetDomain() → "example.com"
    //           new Uri("https://sub.example.com").GetDomain()      → "sub.example.com"

    // TODO: AppendPath(this Uri uri, string segment) : Uri
    //   Descrizione: aggiunge un segmento al path dell'URI, gestendo gli slash
    //   in modo che non vengano duplicati né omessi.
    //   Esempi: new Uri("https://example.com/api").AppendPath("users")  → "https://example.com/api/users"
    //           new Uri("https://example.com/api/").AppendPath("/users") → "https://example.com/api/users"

    // TODO: AddQueryParam(this Uri uri, string key, string value) : Uri
    //   Descrizione: aggiunge (o sostituisce) un parametro querystring all'URI.
    //   Parametri: key — nome del parametro; value — valore (verrà URL-encoded).
    //   Esempi: new Uri("https://example.com/search").AddQueryParam("q", "hello world")
    //               → "https://example.com/search?q=hello+world"
    //           new Uri("https://example.com?page=1").AddQueryParam("size", "20")
    //               → "https://example.com?page=1&size=20"

    // TODO: GetQueryParam(this Uri uri, string key) : string?
    //   Descrizione: restituisce il valore del parametro querystring indicato,
    //   oppure null se non presente.
    //   Esempi: new Uri("https://example.com?page=2&size=10").GetQueryParam("page") → "2"
    //           new Uri("https://example.com").GetQueryParam("page")                → null

    // TODO: RemoveQueryParam(this Uri uri, string key) : Uri
    //   Descrizione: rimuove un parametro querystring dall'URI se presente.
    //   Esempi: new Uri("https://example.com?page=1&size=10").RemoveQueryParam("page")
    //               → "https://example.com?size=10"
    //           new Uri("https://example.com?page=1").RemoveQueryParam("page")
    //               → "https://example.com"
}
