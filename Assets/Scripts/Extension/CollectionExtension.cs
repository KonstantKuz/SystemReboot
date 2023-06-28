using System.Collections.Generic;

namespace Extension
{
    public static class CollectionExtension
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
    }
}