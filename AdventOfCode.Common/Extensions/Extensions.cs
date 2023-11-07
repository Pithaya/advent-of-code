using System.Text.RegularExpressions;

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

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> e)
        {
            return (IEnumerable<T>)e.Where(x => x != null);
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select((item, index) => (item, index));
        }

        public static ulong Sum(this IEnumerable<ulong> enumerable)
        {
            if (!enumerable.Any())
            {
                return 0;
            }

            return enumerable.Aggregate((acc, cur) => acc + cur);
        }

        public static Group[] GetCapturingGroups(this Regex regex, string input)
        {
            var matches = regex.Matches(input);
            return matches.Single().Groups.Values.Skip(1).ToArray();
        }
    }
}
