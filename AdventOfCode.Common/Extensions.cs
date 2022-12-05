using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    public static class Extensions
    {
        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> elements)
        {
            foreach(var e in elements)
            {
                set.Add(e);
            }
        }

        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, valueFactory(key));
            }

            return dic[key];
        }

        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, addValue);
            }
            else
            {
                dic[key] = updateValueFactory(key, dic[key]);
            }
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select((item, index) => (item, index));
        }
    }
}
