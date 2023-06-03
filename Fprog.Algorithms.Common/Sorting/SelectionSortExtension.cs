using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Sorting
{
    public static class SelectionSortExtension
    {
        public static IEnumerable<T> SelectionSort<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            if (collection is null)
                return Array.Empty<T>();

            T[] values = collection.ToArray();

            for (int sortedIndex = 0; sortedIndex < values.Length; sortedIndex++)
            {
                int minIndex = sortedIndex;
                for (int checkingIndex = sortedIndex; checkingIndex < values.Length; checkingIndex++)
                {
                    if (values[checkingIndex].CompareTo(values[minIndex]) < 0)
                        minIndex = checkingIndex;
                }

                T temporary = values[sortedIndex];
                values[sortedIndex] = values[minIndex];
                values[minIndex] = temporary;
            }

            return values;
        }
    }
}
