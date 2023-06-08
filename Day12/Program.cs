namespace Day12;

using System.ComponentModel;
using Common.Parsing;
using Fprog.Algorithms.Common.Structures;

public static class Program
{
    private static int Part1(Graph<Hill> graph, List<Hill> hills)
    {
        var distances = graph.DijkstrasAlgorithm(hills.Single(hill => hill.StartNode));
        var endNode = hills.Single(hill => hill.EndNode);
        return distances[endNode].BestKnownPath.Count;
    }

    private static int Part2(Graph<Hill> graph, List<Hill> hills)
    {
        var minimalDistance = hills
            .Where(hill => hill.Height == 'S' || hill.Height == 'a')
            .Select(graph.DijkstrasAlgorithm)
            .Where(distances => distances[hills.Single(hill => hill.EndNode)].BestKnownPath != null)
            .Select(distances => distances[hills.Single(hill => hill.EndNode)].BestKnownPath.Count)
            .Min();

        return minimalDistance;
    }

    private static void DoWork()
    {
        Matrix<char> matrixChars = MatrixParse.ParseSingleCharacterMatrix("Assets/data.txt");
        char[] chars = matrixChars.AllValues().ToArray();

        List<Hill> hills = new();
        for (int i = 0; i < chars.Length; i++)
            hills.Add(new Hill(chars[i], i));

        Matrix<Hill> matrix = new(hills.Chunk(matrixChars.ColumnsCount));

        Graph<Hill> graph = new(matrix.AllValues());
        for (int rowIndex = 0; rowIndex < matrix.RowCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < matrix.ColumnsCount; columnIndex++)
            {
                var currentValue = matrix[rowIndex, columnIndex];
                var neighbours = matrix.GetNeighbourIndices(rowIndex, columnIndex);
                foreach (var (row, column) in neighbours)
                {
                    var neighbourValue = matrix[row, column];
                    if (
                        neighbourValue.Height <= currentValue.Height + 1
                        || currentValue.Height == 'S'
                    )
                    {
                        graph.AddEdge(new Edge<Hill>(currentValue, neighbourValue, 1));
                    }
                }
            }
        }
        int endId = graph.Vertices.Single(vertex => vertex.EndNode).Id;

        Console.WriteLine($"Part 1: {Part1(graph, hills)}, Part 2: {Part2(graph, hills)}");
    }

    public static void Main()
    {
        new Thread(DoWork, 4_000_000).Start();
    }
}
