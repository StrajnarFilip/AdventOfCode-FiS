namespace Day12;

using System.ComponentModel;
using Common.Parsing;
using Fprog.Algorithms.Common.Structures;

public static class Program
{
    public static int RowAndColumnIndexToId<T>(Matrix<T> matrix, int rowIndex, int columnIndex)
    {
        return matrix.ColumnsCount * rowIndex + columnIndex;
    }

    public static void DoWork()
    {
        Matrix<char> matrixChars = MatrixParse.ParseSingleCharacterMatrix("Assets/small.txt");
        char[] chars = matrixChars.AllValues().Select(ch => ch == 'E' ? 'z' : ch).ToArray();

        List<Hill> hills = new();
        for (int i = 0; i < chars.Length; i++)
        {
            hills.Add(new Hill(chars[i], i));
        }

        Matrix<Hill> matrix = new(hills.Chunk(matrixChars.ColumnsCount));

        Graph<Hill> graph = new Graph<Hill>(matrix.AllValues());
        for (int rowIndex = 0; rowIndex < matrix.RowCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < matrix.ColumnsCount; columnIndex++)
            {
                var currentValue = matrix[rowIndex, columnIndex];
                var neighbours = matrix.GetNeighbourIndices(rowIndex, columnIndex);
                foreach (var neighbour in neighbours)
                {
                    var neighbourValue = matrix[neighbour.row, neighbour.column];
                    if (neighbourValue.Height <= currentValue.Height + 1 || currentValue.Height == 'S')
                    {
                        graph.AddEdge(new Edge<Hill>(currentValue, neighbourValue, 1));
                    }
                }
            }
        }
        int endId = RowAndColumnIndexToId(matrix, 2, 5);
        var distances = graph.DijkstrasAlgorithm(hills.First(hill => hill.Height == 'S'));
        Console.WriteLine($"Shortest distance is: {distances[hills.First(hill => hill.Id == endId)].BestKnownPath.Count}");
    }
    public static void Main()
    {
        new Thread(DoWork, 400_000_000).Start();
    }
}