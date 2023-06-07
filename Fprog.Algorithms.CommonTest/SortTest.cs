namespace Fprog.Algorithms.CommonTest;

using Fprog.Algorithms.Common.Sorting;

public class SortTest
{
    static readonly int[][] _unsorted = new int[][]
    {
        new int[] { 3, 8, 2, 7, 10, 6, 8, 1, 1, 4, 1, 2 },
        new int[] { 4, 10, 5, 1, 9, 4, 6, 8, 1, 2, 2, 4 },
        new int[] { 7, 5, 1, 6, 5, 8, 1, 7, 7, 6, 10, 4 },
        new int[] { 10, 3, 4, 10, 3, 6, 2, 3, 2, 10, 7, 2 },
        new int[] { 10, 3, 3, 9, 7, 1, 5, 5, 2, 6, 10, 1 },
        new int[] { 1, 1, 7, 1, 2, 9, 6, 4, 1, 5, 10, 5 },
        new int[] { 5, 2, 2, 1, 9, 1, 1, 7, 8, 2, 1, 1 },
        new int[] { 8, 2, 1, 6, 3, 3, 1, 7, 9, 9, 6, 6 },
    };

    [Fact]
    public void BubbleSort()
    {
        Assert.All(
            _unsorted,
            (values) =>
            {
                Assert.Equal(values.Order(), values.BubbleSort());
            }
        );
    }

    [Fact]
    public void SelectionSort()
    {
        Assert.All(
            _unsorted,
            (values) =>
            {
                Assert.Equal(values.Order(), values.SelectionSort());
            }
        );
    }
}
