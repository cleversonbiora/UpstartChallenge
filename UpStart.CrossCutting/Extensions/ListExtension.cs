using System;
using System.Collections.Generic;
using System.Linq;

namespace UpStart.CrossCutting.Extensions
{
    public static class ListExtension
    {
        public static List<T> Repeated<T>(T value, int count)
        {
            List<T> ret = new List<T>(count);
            ret.AddRange(Enumerable.Repeat(value, count));
            return ret;
        }

        public static IReadOnlyList<T> AsReadOnly<T>(this IList<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            // Type-sniff, no need to create a wrapper when collection
            // is an IReadOnlyList<T> *already*.
            IReadOnlyList<T> list = collection as IReadOnlyList<T>;

            // If not null, return that.
            return list;
        }
    }
}
