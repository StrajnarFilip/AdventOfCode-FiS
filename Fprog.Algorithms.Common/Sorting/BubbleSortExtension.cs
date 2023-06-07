using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Sorting
{
    public static class BubbleSortExtension
    {
        public static IEnumerable<T> BubbleSort<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
        {
            if (collection is null)
                return Array.Empty<T>();

            T[] values = collection.ToArray();
            for (int limit = values.Length; limit >= 1; limit--)
            {
                for (int index = 0; index < limit - 1; index++)
                {
                    if (values[index].CompareTo(values[index + 1]) > 0)
                    {
                        T temporary = values[index];
                        values[index] = values[index + 1];
                        values[index + 1] = temporary;
                    }
                }
            }

            return values;
        }
    }
}
