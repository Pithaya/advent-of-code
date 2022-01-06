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
    }
}
