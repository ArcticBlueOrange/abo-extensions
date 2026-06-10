namespace AboExtensions.Dictionaries;

public static class DictionaryExtensions
{
    // TODO: GetWithDefault<K,V>(this IReadOnlyDictionary<K,V> d, K key, V defaultValue = default) : V
    //   Descrizione: restituisce il valore associato a key, oppure defaultValue se la chiave
    //   non è presente. Alternativa null-safe a d[key].
    //   Esempi: new Dictionary<string,int>{{"a",1}}.GetOrDefault("a")      → 1
    //           new Dictionary<string,int>{{"a",1}}.GetOrDefault("b")      → 0
    //           new Dictionary<string,int>{{"a",1}}.GetOrDefault("b", -1)  → -1

    public static Dictionary<K, V> AddOrUpdate<K, V>(this Dictionary<K, V> d, K k, V v)
    {
        d[k] = v;
        return d;
    }

    // TODO: Merge<K,V>(this Dictionary<K,V> d, Dictionary<K,V> other,
    //                  bool overwrite = true) : Dictionary<K,V>
    //   Descrizione: restituisce un nuovo dizionario con le coppie di entrambi.
    //   Se overwrite = true (default), le chiavi di other sovrascrivono quelle di d.
    //   Se overwrite = false, le chiavi di d hanno precedenza.
    //   Non muta i dizionari originali.
    //   Esempi: {a:1}.Merge({a:99, b:2})              → {a:99, b:2}
    //           {a:1}.Merge({a:99, b:2}, overwrite: false) → {a:1, b:2}

    public static Dictionary<V, K> Invert<V, K>(
        this Dictionary<K, V> d,
        bool throwOnDuplicate = true)
    {
        var ret = new Dictionary<V, K>();
        foreach (var i in d)
        {
            var nk = i.Value;
            var nv = i.Key;

            if (nk == null)
                throw new ArgumentNullException(nameof(nk));

            if (ret.TryGetValue(nk, out K? value) && throwOnDuplicate)
                throw new ArgumentException($"Duplicate Key: {value}");
            ret[nk] = nv;
        }
        return ret;
    }
}
