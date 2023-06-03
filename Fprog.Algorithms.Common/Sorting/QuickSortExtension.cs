using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Sorting
{
    public static class QuickSortExtension
    {
        public static IEnumerable<T> QuickSort<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            if (collection is null)
                return Array.Empty<T>();

            T[] values = collection.ToArray();

            return values;
        }
    }
}
