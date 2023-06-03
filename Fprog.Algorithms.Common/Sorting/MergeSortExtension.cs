using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Sorting
{
    public static class MergeSortExtension
    {
        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            if (collection is null)
                return Array.Empty<T>();

            int count = collection.Count();

            if (count == 1)
                return collection;


            IEnumerable<T> firstPart = collection.Take(count / 2);
            IEnumerable<T> lastPart = collection.Skip(count / 2);

            var firstSolved = firstPart.MergeSort();
            var lastSolved = lastPart.MergeSort();

            // If first one is empty, return the last part.
            if (!firstSolved.Any())
                return lastSolved;
            // If last part is empty, return the first part.
            if (!lastSolved.Any())
                return firstSolved;


            if(firstSolved.Count() < lastSolved.Count())
            {

            }
            else
            {

            }

            throw new NotImplementedException();
        }
    }
}
