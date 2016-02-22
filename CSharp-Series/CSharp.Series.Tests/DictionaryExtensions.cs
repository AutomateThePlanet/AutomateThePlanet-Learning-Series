using System.Collections.Generic;

namespace CSharp.Series.Tests
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key)
        {
            TValue result;
            return dic.TryGetValue(key, out result) ? result : default(TValue);
        }
    }
}
