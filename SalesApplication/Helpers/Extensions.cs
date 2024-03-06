using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication
{
    public static class Extensions
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
    }
}