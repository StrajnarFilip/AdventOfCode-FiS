using Fprog.Algorithms.Common.Structures;
using Fprog.Algorithms.Common;

Matrix<int> x = new(new int[][]{
    new int[]{ 1,2,3},
    new int[]{ 1,5,3}
});

Console.WriteLine(x.GetNeighbourIndices(0,1, true).FormattedString());

Console.WriteLine($"{x.GetElement(0,1)}");