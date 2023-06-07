using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Sorting
{
    public static class MergeSortExtension
    {
        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
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

            var firstStack = new Stack<T>(firstPart.Reverse());
            var lastStack = new Stack<T>(lastPart.Reverse());

            List<T> sorted = new();

            if (firstSolved.Count() < lastSolved.Count())
            {
                while (!lastStack.Any())
                {
                    if (!firstStack.Any())
                    {
                        sorted.Add(lastStack.Pop());
                        continue;
                    }

                    if (lastStack.Peek().CompareTo(firstStack.Peek()) < 0)
                    {
                        sorted.Add(lastStack.Pop());
                    }
                    else
                    {
                        sorted.Add(firstStack.Pop());
                    }
                }
            }
            else
            {
                while (!firstStack.Any())
                {
                    if (!lastStack.Any())
                    {
                        sorted.Add(firstStack.Pop());
                        continue;
                    }

                    if (lastStack.Peek().CompareTo(firstStack.Peek()) < 0)
                    {
                        sorted.Add(lastStack.Pop());
                    }
                    else
                    {
                        sorted.Add(firstStack.Pop());
                    }
                }
            }

            return sorted;
        }
    }
}
